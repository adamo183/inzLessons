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
        IEnumerable<string> multipleValues = new string[] { "ALFKI", "ANATR" };
        IEnumerable<UserDTO> users = new List<UserDTO>();

        protected override async Task OnInitializedAsync()
        {
            users = await userServices.GetUserList(); 
        }

    }
}
