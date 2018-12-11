using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SomeGameAPI.Entities;
using SomeGameAPI.Models;
using SomeGameAPI.Services;

namespace SomeGameAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Returns bearer token if username and password are correct
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody]LoginModel model)
        {
            var user = this.userService.Authenticate(model);
            if (user == null) return this.BadRequest(new { message = "Username or password is incorrect" });
            return this.Ok(user);
        }

        /// <summary>
        /// Returns bearer token if username and password are correct
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("signin")]
        public IActionResult SignIn([FromBody]SigninModel newUser)
        {
            var user = this.userService.Signin(newUser);
            if (user == null) return this.BadRequest(new { message = "User data incorrect or username is owned" });
            return this.Ok(user);
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = this.userService.GetAll();
            return this.Ok(users);
        }
    }
}