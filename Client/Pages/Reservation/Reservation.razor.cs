using inzLessons.Shared.Reservation;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
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

        void OnSlotRender(SchedulerSlotRenderEventArgs args)
        {
            // Highlight today in month view
            if (args.View.Text == "Month" && args.Start.Date == DateTime.Today)
            {
                args.Attributes["style"] = "background: rgba(255,220,40,.2);";
            }

            // Highlight working hours (9-18)
            if ((args.View.Text == "Week" || args.View.Text == "Day") && args.Start.Hour > 8 && args.Start.Hour < 19)
            {
                args.Attributes["style"] = "background: rgba(255,220,40,.2);";
            }
        }
    }
}
