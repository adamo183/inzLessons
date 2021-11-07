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
using inzLessons.Shared.Users;

namespace inzLessons.Server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUsersServices _usersServices;

        public UsersController(IUsersServices groupServices)
        {
            _usersServices = groupServices;
        }

        [Authorize]
        [HttpGet("studentToSelect")]
        public IActionResult GetStudentList()
        {
            var listToRet = _usersServices.GetAwaibleUsersToGroup().Select(x => new UserDTO()
            { Id = x.Id, Name = x.Firstname, Surname = x.Lastname, Username = x.Username }).ToList();

            return Ok(listToRet);
        }

    }
}