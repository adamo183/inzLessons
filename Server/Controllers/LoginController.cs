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
        [HttpGet("checkUserName/{username}")]
        public IActionResult GetCheckUsername(string username)
        {
            try
            {
                bool tmp = _userService.CheckUsername(username);
                return Ok(tmp);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] RegisterRequest registerRequest)
        {
            try
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
                users.Phone = registerRequest.Phone;
                users.RoleId = registerRequest.Role;
                users.Username = registerRequest.Login;
                users.Id = membToAdd.Id;

                _userService.InsertUser(users);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(LoginRequest model)
        {
            var passwordSalt = _userService.GetUserHash(model.Username);
            if (String.IsNullOrEmpty(passwordSalt))
                return BadRequest(new { message = "Username or password is incorrect" });

            model.Password = _userService.HashPassword(model.Password, passwordSalt, 1, 70);

            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }


    }
}
