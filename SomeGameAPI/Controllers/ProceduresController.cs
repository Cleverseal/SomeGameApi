using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
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
        private IHostingEnvironment hostingEnv;

        public string pathToPhotos => $"{this.hostingEnv.ContentRootPath}\\Content\\Images\\Screens\\";
        public string photoFileExtension => ".jpg";

        public ProceduresController(IProcedureService procedureService, IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnv = hostingEnvironment;
            this.procedureService = procedureService;
        }

        /// <summary>
        /// Starting new procedure and returns it`s id
        /// </summary>
        /// <param name="procedure">Procedure to start</param>
        /// <returns>Started procedure`s id</returns>
        [HttpPost("Start")]
        public ActionResult<int> StartProcedure(StartingProcedure procedure)
        {
            var id = this.procedureService.StartProcedure(procedure);
            return this.Ok(id);
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
        /// Get started procedure of this patient
        /// </summary>
        /// <param name="id">Patient`s id</param>
        /// <returns></returns>
        [HttpGet("started/{id}")]
        public ActionResult<Procedure> GetPatientProcedures(int id)
        {
            return this.Ok(this.procedureService.GetActivePatientProcedure(id));
        }


        [HttpGet("heart/{id}")]
        public ActionResult SetHeartrate(int id, int heartrate)
        {
            this.procedureService.SetHeartrate(id, heartrate);
            return this.Ok();
        }
        
        [HttpGet("temperature/{id}")]
        public ActionResult SetTemperature(int id, float temperature)
        {
            this.procedureService.SetTemperature(id, temperature);
            return this.Ok();
        }

        /// <summary>
        /// Allows to get employee's photo by his login (email)
        /// </summary>
        /// <param name="login">Employee's login (e-mail)</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("photo/{id}")]
        public FileStreamResult GetImage(string id)
        {
            FileStream image;
            try
            {
                image = System.IO.File.OpenRead(this.pathToPhotos + id + this.photoFileExtension);
            }
            catch (FileNotFoundException)
            {
                image = System.IO.File.OpenRead(this.pathToPhotos + "0" + this.photoFileExtension);
            }

            return this.File(image, "image/jpg");
        }


        /// <summary>
        /// Allows to upload employee's photo
        /// </summary>
        [HttpPost("photo")]
        public void Upload()
        {
            if (!this.Request.Form.Files.Any()) throw new Exception("File is null");

            var file = this.Request.Form.Files[0];

            using (Stream stream = file.OpenReadStream())
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    var fileContent = binaryReader.ReadBytes((int)file.Length);
                    System.IO.File.WriteAllBytes(this.pathToPhotos + file.FileName + this.photoFileExtension, fileContent);
                }
            }
        }
    }
}
