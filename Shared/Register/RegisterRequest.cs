using inzLessons.Shared.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inzLessons.Shared.Register
{
    public class RegisterRequest
    {
        public RegisterRequest() { }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public int Role { 
            get
            {
                return (int)_role;
            }
            set 
            {
                _role = (RoleEnum)value;
            }
        }

        private RoleEnum _role = RoleEnum.Student;
    }
}
