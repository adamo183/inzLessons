﻿using inzLessons.Server.Services;
using inzLessons.Shared.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzLessons.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        // private readonly ILogger<LoginController> _logger;
        private IUsersServices _userService;

        public UsersController(IUsersServices usersServices)
        {
            _userService = usersServices;
        }

        [Authorize]
        [HttpGet("studentToSelect")]
        public IActionResult GetStudentList()
        {
            var listToRet = _userService.GetAwaibleUsersToGroup().Select(x => new UserDTO()
            { Id = x.Id, Name = x.Firstname, Surname = x.Lastname, Username = x.Username }).ToList();

            return Ok(listToRet);
        }
    }
}