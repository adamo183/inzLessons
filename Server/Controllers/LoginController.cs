using inzLessons.Server.Services;
using inzLessons.Shared;
using inzLessons.Shared.Login;
using inzLessons.Shared.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inzLessons.Common.Models;

namespace inzLessons.Server.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

       // private readonly ILogger<LoginController> _logger;
        private ILoginServices _userService;

        public LoginController(ILoginServices loginServices)
        {
            _userService = loginServices;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] RegisterRequest registerRequest)
        {
            Membership membToAdd = new Membership();
            membToAdd.Login = registerRequest.Login;
            membToAdd.PasswordSalt = _userService.GenerateSalt(30);
            membToAdd.Password = _userService.HashPassword(registerRequest.Password, membToAdd.PasswordSalt, 1, 70);
            _userService.InsertMembership(membToAdd);

            Users users = new Users();
            users.City = registerRequest.City;
            users.Createdate = DateTime.Now;
            users.Email = registerRequest.Email;
            users.Firstname = registerRequest.Firstname;
            users.Lastname = registerRequest.Lastname;
            users.MembershipId = membToAdd.Id;
            users.Phone = registerRequest.Phone;
            users.RoleId = 1;
            users.Username = registerRequest.Login;
            _userService.InsertUser(users);

            return Ok();
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(LoginRequest model)
        {
            //var response = _userService.Authenticate(model);

            //if (response == null)
            //    return BadRequest(new { message = "Username or password is incorrect" });

            //return Ok(response);
            return Ok();
        }
    }
}
