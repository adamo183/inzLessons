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
using inzLessons.Shared.Role;

namespace inzLessons.Server.Controllers
{

    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private IGroupServices _groupServices;

        public GroupController(IGroupServices groupServices)
        {
            _groupServices = groupServices;
        }



    }
}

