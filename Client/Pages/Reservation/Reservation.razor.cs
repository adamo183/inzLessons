using inzLessons.Shared.Reservation;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inzLessons.Client.Pages.Reservation
{
    public partial class Reservation
    {
        RadzenScheduler<Appointment> scheduler;

        List<Appointment> appointments = new List<Appointment>();

        protected override async Task OnInitializedAsync()
        {
        }

        void OnSlotRender()
        {
        }

        async Task OnSlotSelect(SchedulerSlotSelectEventArgs args)
        {
        }

        async Task OnAppointmentSelect(SchedulerAppointmentSelectEventArgs<Appointment> args)
        {
        }

        void OnAppointmentRender(SchedulerAppointmentRenderEventArgs<Appointment> args)
        {
        }
    }
}
