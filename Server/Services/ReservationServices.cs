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
        public List<Reservation> GetReservationsToTeacher(ReservationParams reservationParams, int teacherId);
        public List<Reservation> GetReservationsToStudent(ReservationParams reservationParams, int studentId);
        public bool IsHourAvailableForTeacher(ReservationParams reservationParams, int teacherId);
    }

    public class ReservationServices : IReservationServices
    {
        UnitOfWork _unitOfWork = new UnitOfWork();

        public List<Reservation> GetReservationsToStudent(ReservationParams reservationParams, int studentId)
        {
            var elementsToRet = _unitOfWork.ReservationRespository.Get(x => x.Reservationdate > reservationParams.Start
               && x.ReservationEndDate < reservationParams.End && x.Userid == studentId, z => z.OrderBy(c => c.Reservationdate), "Useringroup,Useringroup.Group,Useringroup.User").ToList();

            return elementsToRet;
        }

        public bool IsHourAvailableForTeacher(ReservationParams reservationParams, int teacherId)
        {
            var elementsToRet = _unitOfWork.ReservationRespository.Get(x=>x.Reservationdate > reservationParams.Start
                && x.ReservationEndDate < reservationParams.End && x.Useringroup.Group.Teacherid == teacherId, includeProperties: "Useringroup,Useringroup.Group").ToList();
            if (elementsToRet.Count() > 0)
            {
                return false;
            }

            return true;
        }

        public List<Reservation> GetReservationsToTeacher(ReservationParams reservationParams, int teacherId)
        { 
            var elementsToRet = _unitOfWork.ReservationRespository.Get(x=>x.Reservationdate > reservationParams.Start
                && x.ReservationEndDate < reservationParams.End && x.Useringroup.Group.Teacherid == teacherId, z=>z.OrderBy(c=>c.Reservationdate), "Useringroup,Useringroup.Group,Useringroup.User").ToList();

            return elementsToRet;
        }

        public void AddReservationToUser(Reservation reservation)
        {
            _unitOfWork.ReservationRespository.Insert(reservation);
            _unitOfWork.Save();
        }
    }
}
