using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inzLessons.Shared.Group;
using inzLessons.Shared.Users;
using Microsoft.AspNetCore.Components;

namespace inzLessons.Client.Pages.GroupManagement
{
    public partial class AddGroupManagement
    {
        LessonsGroupDTO lessonsGroupDTO = new LessonsGroupDTO();
        IEnumerable<UserDTO> users = new List<UserDTO>();
        bool MembersIdsListClear = false;
        bool popup;

        [Parameter]
        public string? Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            users = await userServices.GetUserList();

            if (Id != null)
            {
                var lessonGroupEdit = await groupServices.GetGroupDataById(Id);
                lessonsGroupDTO = new LessonsGroupDTO()
                {
                    Id = lessonGroupEdit.Id,
                    Description = lessonGroupEdit.Description,
                    Name = lessonGroupEdit.Name,
                    MembersIds = lessonGroupEdit.UsersList.Select(x => x.Id).ToList()
                };
            }

            StateHasChanged();
        }

        public async void OnGroupAdd()
        {
            MembersIdsListClear = false;

            if (lessonsGroupDTO.MembersIds == null || lessonsGroupDTO.MembersIds.Count() == 0)
            {
                MembersIdsListClear = true;
                StateHasChanged();
                return;
            }

            if (String.IsNullOrEmpty(Id))
            {
                var status = await groupServices.CreateNewGroup(lessonsGroupDTO);
                if (status)
                {
                    NavigationManager.NavigateTo("/groupManagement");
                }
            }
            else
            {
                var status = await groupServices.EditGroup(lessonsGroupDTO);
                if (status)
                {
                    NavigationManager.NavigateTo("/groupManagement");
                }
            }
        }

    }
}
