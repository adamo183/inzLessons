using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inzLessons.Shared.Users
{
    public class UserDTO
    {
        public UserDTO() { }
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Username { get; set; }
        public string DisplayFullName => $"{Name} {Surname}";
    }
}
