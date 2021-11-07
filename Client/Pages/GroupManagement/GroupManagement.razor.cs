using inzLessons.Shared.Group;
using Microsoft.AspNetCore.Components;
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

        public void AddNewGroup()
        {
            NavigationManager.NavigateTo("/addGroupManagement");
        }
    }
}
