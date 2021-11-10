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
using inzLessons.Shared.Group;

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

        [Authorize]
        [HttpGet("{Id}")]
        public IActionResult DeleteGroupById(int Id)
        {
            return Ok();
        }

        [Authorize]
        [HttpGet("")]
        public IActionResult GetMyGroup()
        {
            int myId = int.Parse(this.User.FindFirst("id").Value);
            var groupList = _groupServices.GetLessonsgroups(myId);
            List<GroupWithUsersDTO> lessonsGroups = new List<GroupWithUsersDTO>();
            if (groupList != null)
            {
                foreach (var item in groupList)
                {
                    GroupWithUsersDTO groupToAdd = new GroupWithUsersDTO();
                    groupToAdd.Id = item.Id;
                    groupToAdd.Name = item.Name;
                    groupToAdd.Description = item.Description;
                    groupToAdd.UsersList = _groupServices.GetUsersInGroup(item.Id).Select(x => new UserDTO() { Id = x.Id, Name = x.Firstname, Surname = x.Lastname, Username = x.Username }).ToList();
                    lessonsGroups.Add(groupToAdd);
                }
            }

            return Ok(lessonsGroups);
        }

        [Authorize]
        [HttpGet("{Id}")]
        public IActionResult GetGroupById(int Id)
        {
            var groupList = _groupServices.GetGroupById(Id);
            if (groupList != null)
            {
                GroupWithUsersDTO groupToAdd = new GroupWithUsersDTO();
                groupToAdd.Id = groupList.Id;
                groupToAdd.Name = groupList.Name;
                groupToAdd.Description = groupList.Description;
                groupToAdd.UsersList = _groupServices.GetUsersInGroup(groupList.Id).Select(x => new UserDTO() { Id = x.Id, Name = x.Firstname, Surname = x.Lastname, Username = x.Username }).ToList();
                return Ok(groupToAdd);
            }

            return StatusCode(500);
        }

        [Authorize]
        [HttpPost("")]
        public IActionResult PostInsertGroup(LessonsGroupDTO groupToAdd)
        {

            Lessonsgroup lessonsgroup = new Lessonsgroup();
            lessonsgroup.Creationdate = DateTime.Now;
            lessonsgroup.Name = groupToAdd.Name;
            lessonsgroup.Description = groupToAdd.Description;
            lessonsgroup.Teacherid = int.Parse(this.User.FindFirst("id").Value);
            _groupServices.AddGroup(lessonsgroup);
            if (lessonsgroup.Id > 0)
            {
                foreach (var item in groupToAdd.MembersIds)
                {
                    Useringroup useringroup = new Useringroup();
                    useringroup.Groupid = lessonsgroup.Id;
                    useringroup.Userid = item;

                    _groupServices.AddUserInGroup(useringroup);
                }

            }

            return Ok();
        }

        [Authorize]
        [HttpPut("")]
        public IActionResult PutEditGroup(LessonsGroupDTO groupToAdd)
        {

            Lessonsgroup lessonsgroup = _groupServices.GetGroupById(groupToAdd.Id);
            lessonsgroup.Name = groupToAdd.Name;
            lessonsgroup.Description = groupToAdd.Description;
            _groupServices.EditGroup(lessonsgroup);

            var actualUserInGroup = _groupServices.GetUsersInGroup(groupToAdd.Id).Select(x => x.Id).ToList();
            foreach (var item in groupToAdd.MembersIds.ToList())
            {
                if (!actualUserInGroup.Contains(item))
                {
                    Useringroup useringroup = new Useringroup();
                    useringroup.Groupid = groupToAdd.Id;
                    useringroup.Userid = item;
                    _groupServices.AddUserInGroup(useringroup);
                }
            }

            List<int> usersOnServer = lessonsgroup.Useringroup.Select(x => x.Userid).ToList();
            foreach (var item in usersOnServer)
            {
                if (!groupToAdd.MembersIds.Contains(item))
                {
                    var userToDelete = _groupServices.GetUseringroup(lessonsgroup.Id, item);
                    if (userToDelete != null)
                    {
                        _groupServices.DeleteUserInGroup(userToDelete);
                    }
                }
            }

            return Ok();
        }
    }
}

