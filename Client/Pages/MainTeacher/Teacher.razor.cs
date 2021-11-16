using inzLessons.Shared.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzLessons.Client.Pages.MainTeacher
{
    public partial class Teacher
    {
        List<ReservationDTO> reservationList = new List<ReservationDTO>();

        protected override async Task OnInitializedAsync()
        {
            ReservationParams reservation = new ReservationParams();
            reservation.Start = DateTime.Now;
            reservation.End = reservation.Start.AddDays(25);
            reservationList = await reservationServices.GetTeacherReservation(reservation);
            StateHasChanged();
        }
    }
}
