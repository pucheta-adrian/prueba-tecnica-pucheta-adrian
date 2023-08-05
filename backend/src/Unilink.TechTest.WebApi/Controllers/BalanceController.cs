using System.Web.Http;
using System.Threading.Tasks;
using System.Web.Http.Results;

using Unilink.TechTest.Domain.DTOs;
using Unilink.TechTest.WebApi.Services;

namespace Unilink.TechTest.WebApi.Controllers
{
    public class BalanceController : ApiController
    {
        [HttpGet]
        public async Task<JsonResult<BalanceDto>> Get(string id) { 
            var result = await ServicesProvider.RouletteService.GetBalance( id );
            return Json( result );
        }

        [HttpPost]
        public async Task<JsonResult<BalanceDto>> Save(BalanceRequest balance)
        {
            var result = await ServicesProvider.RouletteService.SaveBalance(balance.username, balance.amount);
            return Json(result);
        }
    }
}