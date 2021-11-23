using Common.DAL.UnitOfWork;
using inzLessons.Common.Models;
using inzLessons.Shared.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzLessons.Server.Services
{
    public interface IReservationServices
    {
        public void AddReservationToUser(Reservation reservation);
        public void AddUserInReservation(Userinreservation userinreservation);
        public List<Reservation> GetReservationsToTeacher(ReservationParams reservationParams, int teacherId);
        public List<Reservation> GetReservationsToStudent(ReservationParams reservationParams, int studentId);
        public bool IsHourAvailableForTeacher(ReservationParams reservationParams, int teacherId);
    }

    public class ReservationServices : IReservationServices
    {
        UnitOfWork _unitOfWork = new UnitOfWork();

        public void AddUserInReservation(Userinreservation userinreservation)
        {
            _unitOfWork.UserInReservationRepository.Insert(userinreservation);
            _unitOfWork.Save();
        }

        public List<Reservation> GetReservationsToStudent(ReservationParams reservationParams, int studentId)
        {
            var elementsToRet = _unitOfWork.ReservationRespository.Get(x => x.Reservationdate > reservationParams.Start
               && x.ReservationEndDate < reservationParams.End && x.Userinreservation.Select(x=>x.Userid).ToList().Contains(studentId), z => z.OrderBy(c => c.Reservationdate), "Userinreservation,Userinreservation.Useringroup,Userinreservation,Userinreservation.Useringroup.User").ToList();

            return elementsToRet;
        }

        public bool IsHourAvailableForTeacher(ReservationParams reservationParams, int teacherId)
        {
            var elementsToRet = _unitOfWork.ReservationRespository.Get(x=>x.Reservationdate > reservationParams.Start
                && x.ReservationEndDate < reservationParams.End && x.Teacherid == teacherId, includeProperties: "Userinreservation,Userinreservation.Useringroup,Userinreservation,Userinreservation.Useringroup.User").ToList();
            if (elementsToRet.Count() > 0)
            {
                return false;
            }

            return true;
        }

        public List<Reservation> GetReservationsToTeacher(ReservationParams reservationParams, int teacherId)
        { 
            var elementsToRet = _unitOfWork.ReservationRespository.Get(x=>x.Reservationdate > reservationParams.Start
                && x.ReservationEndDate < reservationParams.End && x.Teacherid == teacherId, z=>z.OrderBy(c=>c.Reservationdate), "Userinreservation,Userinreservation.Useringroup,Userinreservation,Userinreservation.Useringroup.User").ToList();

            return elementsToRet;
        }

        public void AddReservationToUser(Reservation reservation)
        {
            _unitOfWork.ReservationRespository.Insert(reservation);
            _unitOfWork.Save();
        }
    }
}
