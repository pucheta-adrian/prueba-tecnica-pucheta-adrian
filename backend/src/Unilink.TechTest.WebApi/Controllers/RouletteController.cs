using System.Web.Http;
using System.Web.Http.Results;
using System.Threading.Tasks;

using Unilink.TechTest.Domain.DTOs;
using Unilink.TechTest.WebApi.Services;

namespace Unilink.TechTest.WebApi.Controllers
{
    public class RouletteController : ApiController
    {
        [HttpPost]
        public async Task<JsonResult<GameResultDto>> Post(BetRequest request)
        {
            var result = await ServicesProvider.RouletteService.Game(
                request.username, 
                request.betAmount, 
                request.number, 
                request.numberEven, 
                request.color
            );
            var balance = await ServicesProvider.RouletteService.SaveBalance(request.username, result.Amount);
            result.Balance = balance;
            return Json(result);
        }
    }
}
