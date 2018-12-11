using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SomeGameAPI.Entities;
using SomeGameAPI.Models;
using SomeGameAPI.Services;

namespace SomeGameAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [EnableCors("AllowAllHeaders")]
    public class UsersController : ControllerBase
    {
        private IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Returns user model with bearer token if username and password exist
        /// </summary>
        /// <param name="creds">Login model</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<User> Authenticate([FromBody]LoginModel creds)
        {
            var user = this.userService.Login(creds);
            if (user == null) return this.BadRequest(new { message = "Username or password is incorrect" });
            return this.Ok(user);
        }

        /// <summary>
        /// Creates a new user and returns his model (with auth token) 
        /// </summary>
        /// <param name="newUser">Sign in model</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("signin")]
        public ActionResult<User> SignIn([FromBody]SigninModel newUser)
        {
            var user = this.userService.Signin(newUser);
            if (user == null) return this.BadRequest(new { message = "User data incorrect or username is owned" });
            return this.Ok(user);
        }

        /// <summary>
        /// Returns list of all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<User>> GetAll()
        {
            var users = this.userService.GetAll();
            return this.Ok(users);
        }

        /// <summary>
        /// Update user info
        /// </summary>
        /// <param name="user">User model</param>
        [HttpPut("save")]
        public ActionResult UpdateUser([FromBody]User user)
        {
            if (!this.userService.UpdateUser(user)) return this.BadRequest(new { message = "Missing id" });
            return this.Ok();
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            if (!this.userService.DeleteUser(id)) return this.BadRequest(new { message = "Missing id" });
            return this.Ok();
        }
    }
}