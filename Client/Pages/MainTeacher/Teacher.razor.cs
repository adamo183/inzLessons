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
            ReservationParams param = new ReservationParams();
            param.Start = DateTime.Now;
            param.End = param.Start.AddDays(14);
            reservationList = await reservationServices.GetTeacherReservation(param);
        }
    }
}
