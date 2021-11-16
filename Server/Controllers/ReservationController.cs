using inzLessons.Common.Models;
using inzLessons.Server.Services;
using inzLessons.Shared.Reservation;
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
    public class ReservationController : ControllerBase
    {
        private IReservationServices _reservationServices;

        public ReservationController(IReservationServices reservationServices)
        {
            _reservationServices = reservationServices;
        }

        [Authorize]
        [HttpPost("IsHourAvailable")]
        public IActionResult PostTeacherHourAvailable(ReservationParams reservationToAdd)
        {
            int myId = int.Parse(this.User.FindFirst("id").Value);
            var status = _reservationServices.IsHourAvailableForTeacher(reservationToAdd, myId);
            return Ok(status);
        }

        [Authorize]
        [HttpPost("Student")]
        public IActionResult PostStudentReservation(ReservationParams reservationToAdd)
        {
            try
            {
                int myId = int.Parse(this.User.FindFirst("id").Value);
                var listToRet = _reservationServices.GetReservationsToStudent(reservationToAdd, myId)
                    .Select(x => new ReservationDTO()
                    {
                        End = x.ReservationEndDate,
                        Start = x.Reservationdate,
                        GroupId = x.Groupid,
                        UserId = x.Userid,
                        IsOnline = x.Isonline.GetValueOrDefault(),
                        Description = $"{x.Useringroup.User.Firstname} {x.Useringroup.User.Lastname}"
                    });
                return Ok(listToRet);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpPost("Teacher")]
        public IActionResult PostTeacherReservation(ReservationParams reservationToAdd)
        {
            try
            {
                int myId = int.Parse(this.User.FindFirst("id").Value);
                var listToRet = _reservationServices.GetReservationsToTeacher(reservationToAdd, myId)
                    .Select(x => new ReservationDTO()
                    {
                        End = x.ReservationEndDate,
                        Start = x.Reservationdate,
                        GroupId = x.Groupid,
                        UserId = x.Userid,
                        IsOnline = x.Isonline.GetValueOrDefault(),
                        Description = $"{x.Useringroup.User.Firstname} {x.Useringroup.User.Lastname}"
                    });
                return Ok(listToRet);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpPost("")]
        public IActionResult PostInsertReservation(ReservationDTO reservationToAdd)
        {
            Reservation reservation = new Reservation();
            reservation.Groupid = reservationToAdd.GroupId;
            reservation.Userid = reservationToAdd.UserId;
            reservation.Reservationdate = reservationToAdd.Start;
            reservation.ReservationEndDate = reservationToAdd.End;
            reservation.Isonline = reservationToAdd.IsOnline;
            _reservationServices.AddReservationToUser(reservation);
            return Ok();
        }
    }
}
