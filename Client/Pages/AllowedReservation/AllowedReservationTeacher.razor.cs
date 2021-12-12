using inzLessons.Shared.AllowedReservation;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzLessons.Client.Pages.AllowedReservation
{
    public partial class AllowedReservationTeacher
    {
        RadzenScheduler<AllowedReservationDTO> scheduler = new RadzenScheduler<AllowedReservationDTO>();
        IList<AllowedReservationDTO> appointments = new List<AllowedReservationDTO>();

        protected override async Task OnInitializedAsync()
        {
            appointments = await allowedReservationServices.GetAllowedReservationsToTeacher();
            StateHasChanged();
        }

        void OnAppointmentRender(SchedulerAppointmentRenderEventArgs<AllowedReservationDTO> args)
        {
        }

        async Task OnSlotSelect(SchedulerSlotSelectEventArgs args)
        {

            AllowedReservationDTO data = await DialogService.OpenAsync<AddNewAllowedTime>("AddNewAllowedTime"
                 , new Dictionary<string, object> { { "Start", args.Start.AddHours(-args.Start.Hour).AddHours(8) }, { "End", args.Start.AddHours(-args.Start.Hour).AddHours(20) }, { "MaxHourPerStudent", 1 }, { "Id", 0 } });

            if (data != null)
            {
                appointments.Add(data);
                await scheduler.Reload();
            }
        }

        async Task OnAppointmentSelect(SchedulerAppointmentSelectEventArgs<AllowedReservationDTO> args)
        {
            AllowedReservationDTO data = await DialogService.OpenAsync<AddNewAllowedTime>("Edit Appointment",
                new Dictionary<string, object> {
                    { "Start", args.Data.StartTime },
                    { "End", args.Data.EndTime},
                    { "MaxHourPerStudent", args.Data.MaxLessonTimePerStudent },
                    { "Id", args.Data.Id } });

            if (data != null)
            {
                appointments.Where(x => x.Id == data.Id).FirstOrDefault().StartTime = args.Data.StartTime;
                appointments.Where(x => x.Id == data.Id).FirstOrDefault().EndTime = args.Data.EndTime;
                appointments.Where(x => x.Id == data.Id).FirstOrDefault().MaxLessonTimePerStudent = args.Data.MaxLessonTimePerStudent;

                await scheduler.Reload();
            }
        }
    }
}
