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
    public class GroupController : ControllerBase
    {
        public GroupController()
        {

        }
    }
}

