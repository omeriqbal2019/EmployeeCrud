using System;
using Employee.API.Extensions;
using Employee.Models.ApiModelsDto.UserDto;
using Employee.Services.EmployeeService;
using Employee.Utilities.logManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Employee.API.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ApiControllerExtension
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILoggerManager _log;

        public EmployeeController(IEmployeeService employeeService, ILoggerManager logs)
        {
            _employeeService = employeeService;
            _log = logs;
        }
        [HttpGet]
        public IActionResult GetEmployeebyId(int empid)
        {
            try
            {
                var listEmpData = _employeeService.GetEmployeeById(empid);
                if (listEmpData != null)
                    return Ok(listEmpData);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                _log.WriteErrorMsg(controllerName, actionName, ex.Message);
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetAllEmployeesList()
        {
            try
            {
                var listEmpData = _employeeService.GetAllEmployee();
                if (listEmpData != null)
                    return Ok(listEmpData);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                _log.WriteErrorMsg(controllerName, actionName, ex.Message);
                return BadRequest();
            }
            return BadRequest();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult RegisterEmployee(EmployeeDto empDto)
        {
            try
            {
                int? insertedBy=null;
               var profile = GetProfile();
                if (profile != null) 
                    insertedBy = profile.EmployeeId;


                var addEmpData = _employeeService.SignUpEmployee(empDto, insertedBy);
                if (addEmpData != null)
                    return Ok(addEmpData);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                _log.WriteErrorMsg(controllerName, actionName, ex.Message);
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateEmployeeInfo(int id,EmployeeDto empDto)
        {
            try
            {
                var profile = GetProfile();
                var updateEmployeeData = _employeeService.UpdateEmployee(empDto, id, profile.EmployeeId);
                if (updateEmployeeData != null)
                    return Ok(updateEmployeeData);


            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                _log.WriteErrorMsg(controllerName, actionName, ex.Message);

                return BadRequest();

            }

            return BadRequest();
        }

        [HttpDelete]
        public IActionResult DeleteEmployee(int employeeId)
        {
            try
            {
                var profile = GetProfile();
                var deleteEmployee = _employeeService.DeleteEmployee(employeeId, profile.EmployeeId);
                if (deleteEmployee != null)
                    return Ok(deleteEmployee);
            }
            catch (Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                _log.WriteErrorMsg(controllerName, actionName, ex.Message);
                return BadRequest();
            }
            return BadRequest();
        }

    }
}
