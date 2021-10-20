using inzLessons.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace inzLessons.Client.Services
{
using System.Threading.Tasks;
    public interface ILoginServices
    {
        public void UserLogin(LoginRequest param);
        public void RegisterUser();
    }

    public class LoginServices : ILoginServices
    {
        public void RegisterUser() { }
        public void UserLogin(LoginRequest param) { }
    }
}
