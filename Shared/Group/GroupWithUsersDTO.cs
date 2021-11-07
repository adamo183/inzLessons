using inzLessons.Shared.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inzLessons.Shared.Group
{
    public class GroupWithUsersDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<UserDTO> UsersList { get; set; }
        public GroupWithUsersDTO()
        {
            UsersList = new List<UserDTO>();
        }
    }
}
