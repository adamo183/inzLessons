using inzLessons.Common.Models;
using inzLessons.Server.Services;
using inzLessons.Shared.AllowedReservation;
using inzLessons.Shared.ReservationRequest;
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
        [HttpPost("Request")]
        public IActionResult PostAddReservationRequest(ReservationRequestDTO reservationrequest)
        {
            Reservationrequest requToAdd = new Reservationrequest();
            requToAdd.Userid = reservationrequest.UserId;
            requToAdd.Allowedreservationid = reservationrequest.AllowedReservationId;
            requToAdd.Description = reservationrequest.Description;
            requToAdd.Starttime = reservationrequest.StartTime;
            requToAdd.Endtime = reservationrequest.EndTime;
            requToAdd.Isaccepted = false;
            _allowedReservationServices.AddReservationRequest(requToAdd);
            return Ok(true);
        }

        [Authorize]
        [HttpGet("UserRequest")]
        public IActionResult GetReservationRequestToUser()
        {
            try {
                var teacherId = int.Parse(User.FindFirst("id").Value);
                var listToRet = _allowedReservationServices.GetReservationrequestsToUser(teacherId)
                    .Select(x => new ReservationRequestDTO()
                    {
                        AllowedReservationId = x.Allowedreservationid.GetValueOrDefault(),
                        Description = x.Description,
                        EndTime = x.Endtime,
                        StartTime = x.Endtime,
                        TeacherName = x.Allowedreservation.Teacher.Firstname + " " + x.Allowedreservation.Teacher.Lastname,
                        StudentName = x.User.Firstname + " " + x.User.Lastname,
                        TeacherId = x.Allowedreservation.Teacherid,
                        UserId = x.Userid.GetValueOrDefault()
                    });

                return Ok(listToRet);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpGet("Teacher/{Id}")]
        public IActionResult GetAllowedReservationToTeacherById(int Id)
        {
            var modelList = _allowedReservationServices.GetAllowedreservationsToTeacher(Id).Select(
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
