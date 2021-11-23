using inzLessons.Shared.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzLessons.Client.Pages.MainStudent
{
    public partial class Student
    {
        List<ReservationDTO> reservationList = new List<ReservationDTO>();
        IEnumerable<DisplayReservationDTO> reservationToDisplay = new List<DisplayReservationDTO>();
        IList<DisplayReservationDTO> selectedReservation;

        protected override async Task OnInitializedAsync()
        {
            ReservationParams reservation = new ReservationParams();
            reservation.Start = DateTime.Now;
            reservation.End = reservation.Start.AddDays(25);
            reservationList = await reservationServices.GetStudentReservation(reservation);
            reservationToDisplay = reservationList.Select(x => new DisplayReservationDTO()
            {
                Day = x.Start.ToShortDateString(),
                StartTime = x.Start.ToShortTimeString(),
                EndTime = x.End.ToShortTimeString(),
                IsOnline = x.IsOnline,
                Student = x.StudentName,
                Id = x.Id,
                Description = x.Description
            }).ToList();

            StateHasChanged();
        }
    }
}
