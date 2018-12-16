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
    public class PatientsController : ControllerBase
    {
        private IPatientService patientService;

        public PatientsController(IPatientService patientService)
        {
            this.patientService = patientService;
        }
        
        /// <summary>
        /// Adds new patient
        /// </summary>
        /// <param name="patient">New patient</param>
        /// <returns></returns>
        [HttpPost("Add")]
        public ActionResult AddPatient([FromBody]Patient patient)
        {
            this.patientService.AddPatient(patient);
            return this.Ok();
        }

        /// <summary>
        /// Returns patient by his id
        /// </summary>
        /// <param name="id">Patient id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Patient> GetPatient(int id)
        {
            var patient = this.patientService.GetPatient(id);
            if (patient == null) return this.BadRequest();
            return this.Ok(patient);
        }

        /// <summary>
        /// Returns patients list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Patient>> GetPatientsList()
        {
            return this.Ok(this.patientService.GetPatientsList());
        }

        /// <summary>
        /// Updates patient
        /// </summary>
        /// <param name="patient">Updated patient</param>
        /// <returns></returns>
        [HttpPut("Save")]
        public ActionResult UpdatePatient([FromBody]Patient patient)
        {
            if (!this.patientService.UpdatePatient(patient)) return this.BadRequest();
            return this.Ok();
        }

        /// <summary>
        /// Deletes new patient
        /// </summary>
        /// <param name="id">Patient`s id</param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public ActionResult DeletePatient(int id)
        {
            if (!this.patientService.DeletePatient(id)) return this.BadRequest();
            return this.Ok();
        }
    }
}