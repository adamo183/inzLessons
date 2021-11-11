using inzLessons.Shared.Group;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzLessons.Client.Pages.GroupManagement
{
    public partial class GroupManagement
    {
        List<GroupWithUsersDTO> TeacherGroup = new List<GroupWithUsersDTO>();

        protected override async Task OnInitializedAsync()
        {
            TeacherGroup = await groupServices.GetTeacherGroup();
        }

        public async void DeleteGroup(int Id)
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Czy napewno chcesz usunąć?");
            if (confirmed)
            {
                await groupServices.DeleteGroup(Id);
                TeacherGroup = await groupServices.GetTeacherGroup();
                StateHasChanged();
            }
        }

        public void AddNewGroup()
        {
            NavigationManager.NavigateTo("/addGroupManagement");
        }

        public void NavigateToGroupEdit(int GroupId)
        {
            NavigationManager.NavigateTo("/addGroupManagement/" + GroupId);
        }
    }
}
