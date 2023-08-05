using System.Threading.Tasks;
using Unilink.TechTest.Domain.DTOs;
using Unilink.TechTest.Domain.Entities;

namespace Unilink.TechTest.Application.Interfaces
{
    public interface IRouletteService
    {
        RouletteDto PullRoulette();
        Task<GameResultDto> Game(string username, decimal betAmount, int? number, bool? numberEven, RouletteColorEnum? color);
        Task<BalanceDto> SaveBalance(string username, decimal balance);
        Task<BalanceDto> GetBalance(string username);
    }
}