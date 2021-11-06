using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzLessons.Client.Pages.GroupManagement
{
    public partial class GroupManagement
    {
        protected override async Task OnInitializedAsync()
        {
        }

        public void AddNewGroup()
        {
            NavigationManager.NavigateTo("/addGroupManagement");
        }
    }
}
