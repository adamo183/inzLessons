using inzLessons.Shared.Group;
using inzLessons.Shared.Reservation;
using inzLessons.Shared.Users;
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
        List<LessonsGroupDTO> groupList = new List<LessonsGroupDTO>();
        IList<LessonsGroupDTO> selectedGroupList;
        List<UserDTO> userInGroupList = new List<UserDTO>();
        IList<UserDTO> selectedUserList = new List<UserDTO>();

        protected override async Task OnInitializedAsync()
        {
            groupList = await groupServices.GetGroupNamesList();
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
            Appointment data = await DialogService.OpenAsync<AddReservation>("Add Appointment",
            new Dictionary<string, object> { { "Start", args.Start }, { "End", args.End } });

        }

        async Task OnAppointmentSelect(SchedulerAppointmentSelectEventArgs<Appointment> args)
        {
        }

        void OnAppointmentRender(SchedulerAppointmentRenderEventArgs<Appointment> args)
        {
        }
    }
}
