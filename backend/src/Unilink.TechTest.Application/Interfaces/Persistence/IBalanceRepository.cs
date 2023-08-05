using System.Threading.Tasks;
using Unilink.TechTest.Domain.DTOs;

namespace Unilink.TechTest.Application.Interfaces.Persistence
{
    public interface IBalanceRepository
    {
        Task<decimal> GetCurrentBalance(string username);
        Task<BalanceDto> GetBalance(string username);
        Task<BalanceDto> SaveBalance(string username, decimal balance);
    }
}