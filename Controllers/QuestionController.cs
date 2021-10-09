using Microsoft.AspNetCore.Mvc;
using vtb_backend.Models;
using vtb_backend.Services;

namespace vtb_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Header> Get( [FromQuery(Name = "day")] int day, [FromQuery(Name = "header")] int header)
        {
            var mHeader = QuestionService.getDayHeader(day, header);

            if (mHeader == null)
                return NotFound();

            return mHeader;
        }
    }
}