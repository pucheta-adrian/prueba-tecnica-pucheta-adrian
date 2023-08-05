using System;
using System.Threading.Tasks;

using Unilink.TechTest.Domain.DTOs;
using Unilink.TechTest.Domain.Entities;

using Unilink.TechTest.Application.Interfaces;
using Unilink.TechTest.Application.Interfaces.Persistence;

namespace Unilink.TechTest.Application.Services
{
    public class RouletteService : IRouletteService
    {
        private readonly IBalanceRepository _balanceRepository;

        public RouletteService(IBalanceRepository balanceRepository)
        {
            _balanceRepository = balanceRepository;
        }

        public RouletteDto PullRoulette()
        {
            var random = new Random();
            var number = random.Next(0, 36);
            var isEven = (number % 2 == 0);
            var color = (RouletteColorEnum)random.Next(0, 2);
            var game = new RouletteDto  {
                Number = number,
                IsEvenNumber = isEven,
                Color = color
            };

            return game;
        }

        public async Task<GameResultDto> Game(string username, decimal betAmount, int? number, bool? numberEven, RouletteColorEnum? color)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));
            if (betAmount <= 0) throw new ArgumentOutOfRangeException(nameof(betAmount));

            var game = PullRoulette();
            var result = new GameResultDto { Game = game };
            //
            var lost = true;
            var halfBet = (betAmount / 2);
            var tripleBet = (betAmount * 3);
            //
            if (number.HasValue && number.Value == game.Number)
            {
                lost = false;
                result.Win = true;
                result.WinNumber = true;
            }

            if (numberEven.HasValue && numberEven.Value == game.IsEvenNumber)
            {
                lost = false;
                result.Win = true;
                result.WinNumberEven = true;
            }

            if (color.HasValue && color.Value == game.Color)
            {
                lost = false;
                result.Win = true;
                result.WinColor = true;
            }

            if (lost)
                result.Amount = -betAmount;

            if (result.WinColor)
                result.Amount += halfBet;
            
            if (result.WinNumberEven)
                result.Amount += betAmount;

            if (result.WinNumber && result.WinColor)
                result.Amount += tripleBet;

            return await Task.FromResult(result);
        }

        public async Task<BalanceDto> SaveBalance(string username, decimal balance)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));

            var result = await _balanceRepository.SaveBalance(username, balance);

            if (result == null)
                throw new Exception("User's balance could not be saved");

            return result;
        }

        public async Task<BalanceDto> GetBalance(string username)
        {
            return await _balanceRepository.GetBalance(username);
        }
    }
}