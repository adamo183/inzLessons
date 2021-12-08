using inzLessons.Shared.Group;
using inzLessons.Shared.Reservation;
using inzLessons.Shared.Users;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace inzLessons.Client.Pages.Reservation
{
    public partial class Reservation
    {
        RadzenScheduler<Appointment> scheduler;

        List<Appointment> appointments = new List<Appointment>();
        List<LessonsGroupDTO> groupList = new List<LessonsGroupDTO>();
        IList<LessonsGroupDTO> selectedGroupList;
        List<UserDTO> userInGroupList = new List<UserDTO>();
        private IList<UserDTO> selectedUserList = new List<UserDTO>();
        Appointment selectedAppointment = new Appointment();
        ReservationParams param = new ReservationParams();
        string ErrorMessage = "";
        public bool WrongDateError = false;
        private bool _isWholeGroup = false;
        private bool _isEditableUserList = true;
        protected override async Task OnInitializedAsync()
        {
            groupList = await groupServices.GetGroupNamesList();

            var tmpDate = DateTime.Now;
            param.Start = new DateTime(tmpDate.Year, tmpDate.Month, 1);
            param.End = param.Start.AddMonths(1).AddDays(-1);

            var appointmentsTmp = await reservationServices.GetTeacherReservation(param);
            if (appointmentsTmp != null)
            {
                appointments = appointmentsTmp.Select(x => new Appointment()
                {
                    Start = x.Start,
                    End = x.End,
                    Description = String.Join(",", x.Students.Select(x => x.DisplayFullName).ToArray()),
                }).ToList();
            }
            _isWholeGroup = false;
            StateHasChanged();
        }

        void OnSlotRender()
        {
        }

        async void OnGroupChange(DataGridRowMouseEventArgs<LessonsGroupDTO> user)
        {
            var selectedGroupId = user.Data.Id;
            userInGroupList = await userServices.GetUserInGroup(selectedGroupId);
            StateHasChanged();
        }

        async Task OnSlotSelect(SchedulerSlotSelectEventArgs args)
        {
            Appointment data = await dialogService.OpenAsync<AddReservation>("Add Appointment"
                , new Dictionary<string, object> { { "Start", args.Start }, { "End", args.End.AddMinutes(30) } });

            if (data != null)
            {
                selectedAppointment = data;
                await scheduler.Reload();
            }

            StateHasChanged();
        }

        async Task OnAppointmentSelect(SchedulerAppointmentSelectEventArgs<Appointment> args)
        {
        }

        void OnAppointmentRender(SchedulerAppointmentRenderEventArgs<Appointment> args)
        {
        }

        void OnUserChange(UserDTO userDTO)
        {
            selectedUserList.Clear();
            selectedUserList.Add(userDTO);
            StateHasChanged();
        }

        async void AddAppointment()
        {
            WrongDateError = false;
            ErrorMessage = "";
            StateHasChanged();

            if (selectedAppointment == null || selectedAppointment.Start == DateTime.MinValue || selectedAppointment.End == DateTime.MinValue)
            {
                WrongDateError = true;
                ErrorMessage = "Brak wybranej daty";
                StateHasChanged();
                return;
            }

            if (selectedGroupList == null || selectedUserList == null || selectedGroupList.Count == 0 || selectedUserList.Count == 0)
            {
                WrongDateError = true;
                ErrorMessage = "Nie wybrano użytkownika";
                StateHasChanged();
                return;
            }

            ReservationDTO reservationToAdd = new ReservationDTO();
            reservationToAdd.Start = selectedAppointment.Start;
            reservationToAdd.End = selectedAppointment.End;
            reservationToAdd.GroupId = selectedGroupList[0].Id;
            reservationToAdd.UserIds = selectedUserList.Select(x=>x.Id).ToList();
            await reservationServices.AddReservationToUser(reservationToAdd);
            NavigationManager.NavigateTo("/teacher");
        }
    }
}
