using Microsoft.AspNetCore.Mvc;
using vtb_backend.Models;
using vtb_backend.Services;

namespace vtb_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpGet("get/{token}")]
        public ActionResult<User> Get(string token)
        {
            var user = AccountService.GetUser(token);

            if (user == null)
                return NotFound();

            return user;
        }

        [HttpPost("create")]
        public ActionResult<string> Create([Bind("name,email,password")] User user)
        {
            if (user == null)
                return BadRequest();

            return AccountService.CreateUser(user);
        }
    }
}