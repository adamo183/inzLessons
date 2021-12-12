using inzLessons.Common.Models;
using inzLessons.Server.Services;
using inzLessons.Shared.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzLessons.Server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private IMessageServices _messageServices;

        public MessageController(IMessageServices messageServices)
        {
            _messageServices = messageServices;
        }

        [Authorize]
        [HttpGet("{Id}")]
        public IActionResult GetReservationMessage(int id)
        {
            var messageList = _messageServices.GetMessageToReservation(id);
            var messageToRet = messageList.Select(x => new MessageDTO() { Id = x.Id, File = x.File == null ? null : x.File.File, FileName = x.File == null ? null : x.File.Name, ReservationId = x.Reservationid, Text = x.Text, UserId = x.Userid, SenderUser = x.User.Firstname + " " + x.User.Lastname }).ToList();
            return Ok(messageToRet);
        }

        [Authorize]
        [HttpPost()]
        public IActionResult PostReservationMessage(MessageDTO messageDTO)
        {
            messageDTO.UserId = int.Parse(User.FindFirst("id").Value);
            Messagefile fileToAdd = new Messagefile();
            if (!String.IsNullOrEmpty(messageDTO.FileName) && messageDTO.File != null)
            {
                fileToAdd.Name = messageDTO.FileName;
                fileToAdd.File = messageDTO.File;
                _messageServices.InsertMessageFile(fileToAdd);
            }

            Reservationmessage messageToInsert = new Reservationmessage();
            messageToInsert.Reservationid = messageDTO.ReservationId;
            messageToInsert.Text = messageDTO.Text;
            messageToInsert.Creationdate = DateTime.Now;
            messageToInsert.Userid = messageDTO.UserId;
            if (fileToAdd.Id > 0)
            {
                messageToInsert.Fileid = fileToAdd.Id;
            }

            _messageServices.InsertMessage(messageToInsert);
            return Ok(true);
        }
    }
}
