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
    public class ProceduresController : ControllerBase
    {
        private IProcedureService procedureService;

        public ProceduresController(IProcedureService procedureService)
        {
            this.procedureService = procedureService;
        }

        /// <summary>
        /// Starting new procedure 
        /// </summary>
        /// <param name="procedure">Procedure to start</param>
        /// <returns></returns>
        [HttpPost("Start")]
        public ActionResult StartProcedure(StartingProcedure procedure)
        {
            this.procedureService.StartProcedure(procedure);
            return this.Ok();
        }

        /// <summary>
        /// Ends procedure with sended id
        /// </summary>
        /// <param name="id">Id of procedure to end</param>
        /// <returns></returns>
        [HttpPut("End/{id}")]
        public ActionResult EndProcedure(int id)
        {
            if (!this.procedureService.EndProcedure(id)) return this.BadRequest();
            return this.Ok();
        }
        
        /// <summary>
        /// Get all procedures of this user
        /// </summary>
        /// <param name="id">Patient`s id</param>
        /// <returns></returns>
        [HttpGet("PatientProcedures/{id}")]
        public ActionResult GetPatientProcedures(int id)
        {
            return this.Ok(this.procedureService.GetAllPatientProcedures(id));
        }
    }
}
