using Common.DAL.UnitOfWork;
using inzLessons.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzLessons.Server.Services
{
    public interface IMessageServices
    {
        public List<Reservationmessage> GetMessageToReservation(int reservationId);
        public void InsertMessageFile(Messagefile fileToInsert);
        public void InsertMessage(Reservationmessage fileToInsert);
    }

    public class MessageServices : IMessageServices
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        public List<Reservationmessage> GetMessageToReservation(int reservationId)
        {
            var messageToRet = _unitOfWork.MessageRepository.Get(x => x.Reservationid == reservationId, z=>z.OrderBy(c=>c.File), "User,Reservation,File").ToList();
            return messageToRet;
        }

        public void InsertMessageFile(Messagefile fileToInsert)
        {
            _unitOfWork.MessageFileRepository.Insert(fileToInsert);
            _unitOfWork.Save();
        }

        public void InsertMessage(Reservationmessage fileToInsert)
        {
            _unitOfWork.MessageRepository.Insert(fileToInsert);
            _unitOfWork.Save();
        }

    }
}
