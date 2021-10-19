using inzLessons.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzLessons.Client.Services
{
    public interface ILoginServices
    {
        public void UserLogin(LoginRequest param);
    }

    public class LoginServices : ILoginServices
    {
        public void UserLogin(LoginRequest param) { }
    }
}
