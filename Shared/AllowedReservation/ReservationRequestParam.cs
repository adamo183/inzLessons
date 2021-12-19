using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inzLessons.Shared.AllowedReservation
{
    public class ReservationRequestParam
    {
        public ReservationRequestParam() { }
        public int TeacherId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
