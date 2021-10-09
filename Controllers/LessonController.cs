using Microsoft.AspNetCore.Mvc;
using vtb_backend.Models;
using vtb_backend.Services;

namespace vtb_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LessonController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Lesson> Get([FromQuery(Name = "lessonId")] int lessonId)
        {
            return LessonService.getLesson(lessonId);
        }
    }
}