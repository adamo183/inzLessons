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
        public void AddGroup(Lessonsgroup groupToAdd);
        public void AddUserInGroup(Useringroup useringroup);
        public void DeleteUserInGroup(Useringroup useringroup);
        public List<Lessonsgroup> GetLessonsgroups(int userID);
        public List<Users> GetUsersInGroup(int groupId);
        public Lessonsgroup GetGroupById(int groupId);
        public void EditGroup(Lessonsgroup groupToAdd);
        public Useringroup GetUseringroup(int groupId, int userId);
    }

    public class GroupServices : IGroupServices
    {
        UnitOfWork _unitOfWork = new UnitOfWork();

        public Useringroup GetUseringroup(int groupId, int userId)
        {
            var userInGroupToRet = _unitOfWork.UserInGroupRepository.Get(x => x.Groupid == groupId && x.Userid == userId).FirstOrDefault();
            return userInGroupToRet;
        }

        public void DeleteUserInGroup(Useringroup useringroup)
        {
            _unitOfWork.UserInGroupRepository.Delete(useringroup);
            _unitOfWork.Save();
            return;
        }

        public Lessonsgroup GetGroupById(int groupId)
        {
            var groupToRet = _unitOfWork.LessonsGroupRepository.Get(x => x.Id == groupId).FirstOrDefault();
            return groupToRet;
        }

        public List<Lessonsgroup> GetLessonsgroups(int teacherID)
        {
            var groupListToRet = _unitOfWork.LessonsGroupRepository.Get(x => x.Teacherid == teacherID).ToList();
            return groupListToRet;
        }

        public List<Users> GetUsersInGroup(int groupId)
        {
            var usersInGroup = _unitOfWork.UserInGroupRepository.Get(x => x.Groupid == groupId, includeProperties: "User").Select(x=>x.User).ToList();
            return usersInGroup;
        }

        public void AddGroup(Lessonsgroup groupToAdd)
        {
            _unitOfWork.LessonsGroupRepository.Insert(groupToAdd);
            _unitOfWork.Save();
            return;
        }

        public void EditGroup(Lessonsgroup groupToAdd)
        {
            _unitOfWork.LessonsGroupRepository.Update(groupToAdd);
            _unitOfWork.Save();
            return;
        }

        public void AddUserInGroup(Useringroup useringroup)
        {
            _unitOfWork.UserInGroupRepository.Insert(useringroup);
            _unitOfWork.Save();
            return;
        }

    }
}
