using inzLessons.Shared.Reservation;
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

        protected override void OnParametersSet()
        {
            model.Start = Start;
            model.End = End;
        }

        void OnSubmit(Appointment model)
        {
            if (model.End < model.Start)
            {
                WrongDateError = true;
                StateHasChanged();
                return;
            }

            DialogService.Close(model);
        }
    }

}
