using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using vtb_backend.Models;
using vtb_backend.Services;

namespace vtb_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatsController : ControllerBase
    {
        [HttpPost("get")]
        public ActionResult<UserStats> Get([Bind("token")] TokenObject token)
        {
            if (token == null)
                return BadRequest();
            return StatsService.GetStats(token.token);
        }
        
        [HttpPost("set")]
        public void Set([Bind("token")] ApiUserStats apiUserStats)
        {
            StatsService.SetStats(apiUserStats);
        }
    }
}