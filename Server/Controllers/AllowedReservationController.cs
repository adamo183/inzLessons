using inzLessons.Common.Models;
using inzLessons.Server.Services;
using inzLessons.Shared.AllowedReservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzLessons.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AllowedReservationController : ControllerBase
    {
        private IAllowedReservationServices _allowedReservationServices;

        public AllowedReservationController(IAllowedReservationServices allowedReservationServices)
        {
            _allowedReservationServices = allowedReservationServices;
        }

        [Authorize]
        [HttpGet("Teacher")]
        public IActionResult GetAllowedReservationToTeacher()
        {
            var teacherId = int.Parse(User.FindFirst("id").Value);

            var modelList = _allowedReservationServices.GetAllowedreservationsToTeacher(teacherId).Select(
                x => new AllowedReservationDTO()
                {
                    Id = x.Id,
                    EndTime = x.Reservationdateend,
                    StartTime = x.Reservationdatestart,
                    MaxLessonTimePerStudent = x.MaxHourPerStudent.HasValue ? x.MaxHourPerStudent.GetValueOrDefault() : 1,
                    TeacherId = x.Teacherid,
                    TeacherName = x.Teacher.Firstname + " " + x.Teacher.Lastname
                });
            return Ok(modelList);
        }


        [Authorize]
        [HttpPost("")]
        public IActionResult PostAddAllowedReservation(AllowedReservationDTO allowedReservation)
        {
            allowedReservation.TeacherId = int.Parse(User.FindFirst("id").Value);
            Allowedreservation reservationModel = new Allowedreservation();
            reservationModel.Teacherid = allowedReservation.TeacherId;
            reservationModel.Reservationdateend = allowedReservation.EndTime;
            reservationModel.Reservationdatestart = allowedReservation.StartTime;
            reservationModel.MaxHourPerStudent = allowedReservation.MaxLessonTimePerStudent;
            _allowedReservationServices.AddAllowedReservation(reservationModel);
            return Ok(true);
        }

        [Authorize]
        [HttpPut("")]
        public IActionResult PutEditAllowedReservation(AllowedReservationDTO allowedReservation)
        {
            var elemToEdit = _allowedReservationServices.GetAllowedreservationById(allowedReservation.Id);
            if (elemToEdit != null)
            {
                elemToEdit.Reservationdateend = allowedReservation.EndTime;
                elemToEdit.Reservationdatestart = allowedReservation.StartTime;
                elemToEdit.MaxHourPerStudent = allowedReservation.MaxLessonTimePerStudent;
                _allowedReservationServices.EditAllowedreservation(elemToEdit);
            }

            return Ok(true);
        }

        [Authorize]
        [HttpPost("CheckAvailable")]
        public IActionResult PostCheckAllowedReservationAvailable(AllowedReservationDTO allowedReservation)
        {
            allowedReservation.TeacherId = int.Parse(User.FindFirst("id").Value);
            var statusToRet = _allowedReservationServices.IsAllowedReservationAvailable(allowedReservation);

            return Ok(statusToRet);
        }
    }
}
