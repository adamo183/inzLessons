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
        public void AddReservationRequest(Reservationrequest resReq);
        public List<Reservationrequest> GetReservationrequestsToUser(int userId);
        public List<Reservationrequest> GetReservationrequestsToTeacher(int teacherId);
        public void AcceptReservationRequestById(int id);
        public void RejectReservationRequestById(int id);
    }

    public class AllowedReservationServices : IAllowedReservationServices
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public void AcceptReservationRequestById(int id)
        {
            var requestToAccept = _unitOfWork.ReservationRequestRepository.Get(x => x.Id == id).FirstOrDefault();
            if (requestToAccept != null)
            {
                requestToAccept.Isaccepted = true;
                _unitOfWork.ReservationRequestRepository.Update(requestToAccept);
                _unitOfWork.Save();
            }
        }

        public void RejectReservationRequestById(int id)
        {
            var requestToAccept = _unitOfWork.ReservationRequestRepository.Get(x => x.Id == id).FirstOrDefault();
            if (requestToAccept != null)
            {
                requestToAccept.Isaccepted = true;
                _unitOfWork.ReservationRequestRepository.Update(requestToAccept);
                _unitOfWork.Save(); 
            }
        }

        public List<Reservationrequest> GetReservationrequestsToTeacher(int teacherId)
        {
            var teacherAllowedReservationList = _unitOfWork.AllowedReservationRepository.Get(x => x.Teacherid == teacherId).Select(x => x.Id).ToList();
            var reservationRequestList = _unitOfWork.ReservationRequestRepository.Get(x => teacherAllowedReservationList.Contains(x.Allowedreservationid.GetValueOrDefault()) && x.Isaccepted == false
            , includeProperties: "Allowedreservation,Allowedreservation.Teacher,User").ToList();
            return reservationRequestList;
        }

        public List<Reservationrequest> GetReservationrequestsToUser(int userId)
        {
            var listToRet = _unitOfWork.ReservationRequestRepository.Get(x => x.Userid == userId && x.Isaccepted == false
            , includeProperties: "Allowedreservation,Allowedreservation.Teacher,User").ToList();
            return listToRet;
        }

        public void AddReservationRequest(Reservationrequest resReq)
        {
            _unitOfWork.ReservationRequestRepository.Insert(resReq);
            _unitOfWork.Save();
        }

        public List<Reservationrequest> GetReservationRequestToTeacher(ReservationRequestParam param)
        {
            var listToRet = _unitOfWork.ReservationRequestRepository.Get(x => x.Allowedreservation.Teacherid == param.TeacherId
            && x.Starttime > param.Start
            && x.Endtime < param.End, includeProperties: "Allowedreservation").ToList();
            return listToRet;
        }

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
