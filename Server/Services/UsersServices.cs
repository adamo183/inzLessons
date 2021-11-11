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
        public List<Users> GetUsersInGroup(int id);
    }

    public class UsersServices : IUsersServices
    {
        UnitOfWork _unitOfWork = new UnitOfWork();

        public List<Users> GetUsersInGroup(int id)
        {
            var usersList = _unitOfWork.UserInGroupRepository.Get(x => x.Groupid == id, includeProperties: "User").Select(x => x.User).ToList();
            return usersList;
        }

        public List<Users> GetAwaibleUsersToGroup()
        {
            return _unitOfWork.UsersRepository.Get(x => x.RoleId == 2).ToList();
        }
    }
}
