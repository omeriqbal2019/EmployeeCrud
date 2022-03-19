using Employee.Models.ApiModelsDto.AuthenticationDto;
using Employee.Models.ApiModelsDto.ErrorMessageDto;
using Employee.Services.Auth;
using Employee.Utilities.logManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Employee.API.Controllers
{


    [Route("api/[controller]/[action]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticateService;
        private readonly ILoggerManager _log;
        public AuthController(IAuthenticationService authenticateService, ILoggerManager Logs)
        {
            _authenticateService = authenticateService;
            _log=Logs;
        }

    
        [HttpPost]
        public IActionResult Login(LoginDto _credentials)
        {
            try
            {
                var user = _authenticateService.Authenticate(_credentials);
                if (user == null)
                    return Unauthorized();
             
               
                return Ok(user);
            }
            catch(Exception ex)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                _log.WriteErrorMsg(controllerName, actionName, ex.Message);

                return BadRequest();
            }
        }


    }
}
