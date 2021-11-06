using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inzLessons.Shared.Group;
using inzLessons.Shared.Users;

namespace inzLessons.Client.Pages.GroupManagement
{
    public partial class AddGroupManagement
    {
        LessonsGroupDTO lessonsGroupDTO = new LessonsGroupDTO();
        IEnumerable<UserDTO> users = new List<UserDTO>();
        bool MembersIdsListClear = false;
        bool popup;

        protected override async Task OnInitializedAsync()
        {
            users = await userServices.GetUserList();
        }

        public async void OnGroupAdd()
        {
            MembersIdsListClear = false;

            if (lessonsGroupDTO.MembersIds == null || lessonsGroupDTO.MembersIds.Count() == 0)
            {
                MembersIdsListClear = true;
                StateHasChanged();
            }

            var status = await groupServices.CreateNewGroup(lessonsGroupDTO);
            if (status)
            {
                NavigationManager.NavigateTo("/groupManagement");
            }
        }

    }
}
