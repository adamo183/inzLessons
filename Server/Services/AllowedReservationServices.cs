using Common.DAL.UnitOfWork;
using inzLessons.Common.Models;
using inzLessons.Shared.AllowedReservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzLessons.Server.Services
{
    public interface IAllowedReservationServices
    {
        public bool IsAllowedReservationAvailable(AllowedReservationDTO allowedReservation);
        public void AddAllowedReservation(Allowedreservation allowedReservation);
        public List<Allowedreservation> GetAllowedreservationsToTeacher(int userId);
        public Allowedreservation GetAllowedreservationById(int allowResId);
        public void EditAllowedreservation(Allowedreservation allowRes);
    }

    public class AllowedReservationServices : IAllowedReservationServices
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public void EditAllowedreservation(Allowedreservation allowRes)
        {
            _unitOfWork.AllowedReservationRepository.Update(allowRes);
            _unitOfWork.Save();
        }

        public Allowedreservation GetAllowedreservationById(int allowResId)
        {
            var elemToRet = _unitOfWork.AllowedReservationRepository.Get(x => x.Id == allowResId).FirstOrDefault();
            return elemToRet;
        }

        public List<Allowedreservation> GetAllowedreservationsToTeacher(int userId)
        {
            var listToRet = _unitOfWork.AllowedReservationRepository.Get(x => x.Teacherid == userId, includeProperties: "Teacher").ToList();
            return listToRet;
        }

        public void AddAllowedReservation(Allowedreservation allowedReservation)
        {
            _unitOfWork.AllowedReservationRepository.Insert(allowedReservation);
            _unitOfWork.Save();
        }

        public bool IsAllowedReservationAvailable(AllowedReservationDTO allowedReservation)
        {
            var isFree = _unitOfWork.AllowedReservationRepository.Get(x => x.Teacherid == allowedReservation.TeacherId && x.Reservationdatestart >= allowedReservation.StartTime && x.Reservationdateend < allowedReservation.EndTime).ToList();
            if (isFree.Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
