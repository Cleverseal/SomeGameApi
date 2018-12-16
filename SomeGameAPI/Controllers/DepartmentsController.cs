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
    public class DepartmentsController : ControllerBase
    {
        private IDepartmentService departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        /// <summary>
        /// Returns department by id
        /// </summary>
        /// <param name="id">department`s id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Department> GetDepartment(int id)
        {
            return this.departmentService.GetDepartment(id);
        }
    }
}