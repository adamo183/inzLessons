using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inzLessons.Shared.Reservation
{
    public class ReservationDTO
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public ReservationDTO() { }
    }
}
