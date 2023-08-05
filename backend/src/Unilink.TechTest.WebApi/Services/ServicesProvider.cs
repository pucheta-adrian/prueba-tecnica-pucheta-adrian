using Unilink.TechTest.Application.Interfaces;
using Unilink.TechTest.Application.Interfaces.Persistence;
using Unilink.TechTest.Application.Services;
using Unilink.TechTest.Infrastructure.Persistence.Repositories;

namespace Unilink.TechTest.WebApi.Services
{
    public static class ServicesProvider
    {
        private static IBalanceRepository BalanceRepository = new BalanceRepository();
        public static IRouletteService RouletteService = new RouletteService(
            BalanceRepository
        );
    }
}