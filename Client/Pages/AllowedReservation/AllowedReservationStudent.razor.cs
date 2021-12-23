using inzLessons.Shared.AllowedReservation;
using inzLessons.Shared.ReservationRequest;
using inzLessons.Shared.Users;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzLessons.Client.Pages.AllowedReservation
{
    public partial class AllowedReservationStudent
    {
        RadzenScheduler<AllowedReservationDTO> scheduler = new RadzenScheduler<AllowedReservationDTO>();
        IList<AllowedReservationDTO> appointments = new List<AllowedReservationDTO>();
        List<UserDTO> teacherList = new List<UserDTO>();
        List<ReservationRequestDTO> reservationRequest = new List<ReservationRequestDTO>();

        async void OnTeacherChange(object value)
        {
            var teacherToChange = teacherList.Where(x => x.DisplayFullName == (string)value).FirstOrDefault();
            if (teacherToChange != null)
            {
                SelectedTeacher = teacherToChange;
                appointments = await allowedReservationServices.GetAllowedReservationsByTeacherId(SelectedTeacher.Id);
                StateHasChanged();
            }
        }

        private UserDTO _selectedTeacher;
        public UserDTO SelectedTeacher
        {
            get
            {
                return _selectedTeacher;
            }
            set
            {
                if (_selectedTeacher != value)
                {
                    _selectedTeacher = value;
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            teacherList = await reservationServices.GetStudentTeacher();
            if (teacherList != null && teacherList.Count() > 0)
            {
                SelectedTeacher = teacherList.FirstOrDefault();
                appointments = await allowedReservationServices.GetAllowedReservationsByTeacherId(SelectedTeacher.Id);

                StateHasChanged();
            }

            reservationRequest = await allowedReservationServices.GetStudentReservationRequest();

            StateHasChanged();
        }

        async Task OnAppointmentSelect(SchedulerAppointmentSelectEventArgs<AllowedReservationDTO> args)
        {
            ReservationRequestDTO data = await DialogService.OpenAsync<AddReservationRequest>("Add Reservation Request"
     , new Dictionary<string, object> { { "Start", args.Start.AddHours(-args.Start.Hour).AddHours(8) }, 
         { "End", args.Start.AddHours(args.Data.MaxLessonTimePerStudent) }, 
         { "MaxHourPerStudent", args.Data.MaxLessonTimePerStudent },
         { "AllowedHourId", args.Data.Id}
     });

            if (data != null)
            {

                await scheduler.Reload();
                reservationRequest = await allowedReservationServices.GetStudentReservationRequest();
                StateHasChanged();
            }
        }

        public async void RejectRequest(ReservationRequestDTO request)
        {
            await allowedReservationServices.RejectLessonRequest(request);
            reservationRequest = await allowedReservationServices.GetStudentReservationRequest();
            StateHasChanged();
        }
    }
}
