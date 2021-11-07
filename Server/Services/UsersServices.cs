using Common.DAL.UnitOfWork;
using inzLessons.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzLessons.Server.Services
{
    public interface IUsersServices
    {
        public List<Users> GetAwaibleUsersToGroup();
    }



    public class UserServices : IUsersServices
    {
        UnitOfWork _unitOfWork = new UnitOfWork();

        public List<Users> GetAwaibleUsersToGroup()
        {
            return _unitOfWork.UsersRepository.Get(x => x.RoleId == 2).ToList();
        }
    }
}
