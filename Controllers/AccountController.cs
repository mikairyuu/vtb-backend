using Microsoft.AspNetCore.Mvc;
using vtb_backend.Models;
using vtb_backend.Services;

namespace vtb_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpPost("get")]
        public ActionResult<User> Get([Bind("token")] TokenObject token)
        {
            var user = AccountService.GetUser(token.token);

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

        [HttpPost("login")]
        public ActionResult<string> Login([Bind("email,password")] User user)
        {
            if (user == null)
                return BadRequest();
            var loggedUser = AccountService.CreateUser(user);
            if (loggedUser == null)
                return NotFound();

            return loggedUser;
        }
    }
}