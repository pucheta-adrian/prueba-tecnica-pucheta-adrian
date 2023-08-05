using System;
using System.Linq;
using System.Threading.Tasks;

using System.Data.Entity;
using System.Data.Entity.Migrations;


using Unilink.TechTest.Domain.Entities;
using Unilink.TechTest.Application.Interfaces.Persistence;
using Unilink.TechTest.Domain.DTOs;

namespace Unilink.TechTest.Infrastructure.Persistence.Repositories
{
    public class BalanceRepository : IBalanceRepository
    {
        private readonly ApplicationDbContext _appContext = new ApplicationDbContext();

        public async Task<BalanceDto> GetBalance(string username)
        {
            var wallet = await _appContext.Balances
                .Where(item => item.UserName == username)
                
                .FirstOrDefaultAsync() ?? new Balance { UserName = username };

            if (wallet.BalanceId == 0) {
                wallet.UpdatedAt = DateTime.UtcNow;
                _appContext.Balances.Add(wallet);
                await _appContext.SaveChangesAsync();
            }

            return new BalanceDto { username = wallet.UserName, amount = wallet.Amount };
        }

        public async Task<decimal> GetCurrentBalance(string username)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));
            
            decimal balance = 0;
            if (await _appContext.Balances.AnyAsync(item => item.UserName == username))
                balance = await _appContext.Balances
                    .Where(item => item.UserName == username)
                    .Select(item => item.Amount)
                    .FirstOrDefaultAsync();

            return balance;
        }

        public async Task<BalanceDto> SaveBalance(string username, decimal balance)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));

            var wallet = await _appContext.Balances
                .Where(item => item.UserName == username)
                .FirstOrDefaultAsync() ?? new Balance { UserName = username };

            wallet.Amount += balance;
            wallet.UpdatedAt = DateTime.UtcNow;

            _appContext.Balances.AddOrUpdate(wallet);
            await _appContext.SaveChangesAsync();

            return new BalanceDto { username = wallet.UserName, amount = wallet.Amount };
        }
    }
}