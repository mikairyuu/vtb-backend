using Microsoft.AspNetCore.Mvc;
using vtb_backend.Models;
using vtb_backend.Services;

namespace vtb_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatsController
    {
        [HttpGet("get/{token}")]
        public ActionResult<UserStats> Get(string token)
        {
            //TODO
            return null;
        }
    }
}