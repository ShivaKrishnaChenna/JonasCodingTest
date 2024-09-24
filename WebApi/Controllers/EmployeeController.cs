using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;

        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeInfo>> Get()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return employees;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(EmployeeInfo employeeInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var result = await _employeeService.SaveEmployeeAsync(employeeInfo);
            if (!result)
                throw new Exception("Could not Save the Employee");

            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(string employeeCode, EmployeeInfo employeeInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _employeeService.UpdateEmployeeAsync(employeeCode, employeeInfo);
            if (!result)
                throw new Exception("Could not Update the Employee");

            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string employeeCode)
        {

            var result = await _employeeService.DeleteEmployeeAsync(employeeCode);
            if (!result)
                throw new Exception("Could not Delete the Employee");

            return Ok();
        }
    }
}