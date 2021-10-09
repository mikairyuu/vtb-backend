using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using vtb_backend.Models;

namespace vtb_backend.Controllers
{
    public class AchievementsController
    {
        [ApiController]
        [Route("[controller]")]
        public class StatsController
        {
            [HttpPost("markDone/{id}")]
            public ActionResult<List<Achievement>> Done([Bind("token")] TokenObject token)
            {
                //TODO
                return null;
            }
        }
    }
}