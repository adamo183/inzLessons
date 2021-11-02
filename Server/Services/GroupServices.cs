using Common.DAL.UnitOfWork;
using inzLessons.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzLessons.Server.Services
{
    public interface IGroupServices
    {
        public List<Users> GetAwaibleUsersToGroup();
    }

    public class GroupServices : IGroupServices
    {
        UnitOfWork _unitOfWork = new UnitOfWork();

        public List<Users> GetAwaibleUsersToGroup()
        {
            return _unitOfWork.UsersRepository.Get(x => x.RoleId == 2).ToList();
        }
    }
}
