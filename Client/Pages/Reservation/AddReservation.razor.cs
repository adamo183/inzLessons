﻿using inzLessons.Shared.Reservation;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzLessons.Client.Pages.Reservation
{
    public partial class AddReservation
    {
        [Parameter]
        public DateTime Start { get; set; }

        [Parameter]
        public DateTime End { get; set; }

        Appointment model = new Appointment();
        bool WrongDateError = false;
        bool HourNotAvailable = false;

        protected override void OnParametersSet()
        {
            model.Start = Start;
            model.End = End;
        }

         async void OnSubmit(Appointment model)
        {
            HourNotAvailable = false;
            WrongDateError = false;

            if (model.End < model.Start)
            {
                WrongDateError = true;
                StateHasChanged();
                return;
            }

            ReservationParams param = new ReservationParams();
            param.End = model.End;
            param.Start = model.Start;

            if (!(await reservationServices.CheckTeacherHourAvaiable(param)))
            {
                HourNotAvailable = true;
                StateHasChanged();
                return;
            }
            DialogService.Close(model);
        }
    }

}
