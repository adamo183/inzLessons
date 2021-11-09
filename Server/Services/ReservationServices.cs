using Common.DAL.UnitOfWork;
using inzLessons.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzLessons.Server.Services
{
    public interface IReservationServices
    {
        public void AddReservationToUser(Reservation reservation);
    }

    public class ReservationServices : IReservationServices
    {
        UnitOfWork _unitOfWork = new UnitOfWork();

        public void AddReservationToUser(Reservation reservation)
        {
            _unitOfWork.ReservationRespository.Insert(reservation);
            _unitOfWork.Save();
        }
    }
}
