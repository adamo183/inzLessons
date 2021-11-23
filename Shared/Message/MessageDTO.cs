using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inzLessons.Shared.Message
{
    public class MessageDTO
    {
        public MessageDTO() { }
        public long Id { get; set; }
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public string SenderUser { get; set; }
        public byte[] File { get; set; }    
        public string FileName { get; set; }
    }
}
