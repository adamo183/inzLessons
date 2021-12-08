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
                        Id = x.Id,
                        End = x.ReservationEndDate,
                        Start = x.Reservationdate,
                        IsOnline = x.Isonline.GetValueOrDefault(),
                        Students = x.Userinreservation.Select(z => new Shared.Users.UserDTO()
                        {
                            Id = z.Userid,
                            Name = z.Useringroup.User.Firstname,
                            Surname = z.Useringroup.User.Lastname,
                            Username = z.Useringroup.User.Username,
                        }).ToList(),
                        Description = x.Description
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
                        Id = x.Id,
                        End = x.ReservationEndDate,
                        Start = x.Reservationdate,
                        IsOnline = x.Isonline.GetValueOrDefault(),
                        Students = x.Userinreservation.Select(z => new Shared.Users.UserDTO() 
                        {
                            Id = z.Userid,
                            Name = z.Useringroup.User.Firstname,
                            Surname = z.Useringroup.User.Lastname,
                            Username = z.Useringroup.User.Username,
                        }).ToList(),
                        Description = x.Description
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
            reservation.Reservationdate = reservationToAdd.Start;
            reservation.ReservationEndDate = reservationToAdd.End;
            reservation.Isonline = reservationToAdd.IsOnline;
            reservation.Teacherid = int.Parse(this.User.FindFirst("id").Value);
            _reservationServices.AddReservationToUser(reservation);

            foreach (var item in reservationToAdd.UserIds)
            {
                Userinreservation userinreservation = new Userinreservation();
                userinreservation.Reservationid = reservation.Id;
                userinreservation.Groupid = reservationToAdd.GroupId;
                userinreservation.Userid = item;
                _reservationServices.AddUserInReservation(userinreservation);
            }
            
            return Ok();
        }
    }
}
