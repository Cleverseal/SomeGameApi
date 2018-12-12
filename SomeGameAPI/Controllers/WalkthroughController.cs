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
    public class WalkthroughController : ControllerBase
    {
        private IWalktroughService walktroughService;

        public WalkthroughController(IWalktroughService walktroughService)
        {
            this.walktroughService = walktroughService;
        }

        /// <summary>
        /// Returns list of all user walkthroughs
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<List<Walkthrough>> GetUserWalkthroughs(int id)
        {
            var users = this.walktroughService.GetUserWalkthroughs(id);
            return this.Ok(users);
        }

        /// <summary>
        /// Adding a new walkthrough to user
        /// </summary>
        /// <param name="walkthrough">New walkthrough (id can be null, it </param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Add")]
        public ActionResult Add([FromBody]Walkthrough walkthrough)
        {
            this.walktroughService.AddWalkthrough(walkthrough);
            return this.Ok();
        }

        /// <summary>
        /// Returns board with best user results
        /// </summary>
        /// <returns></returns>
        [HttpGet("leaderboard")]
        public ActionResult<List<LeaderboardPosition>> GetLeaderboard()
        {
            var users = this.walktroughService.GetLeaderBoard();
            return this.Ok(users);
        }
    }
}