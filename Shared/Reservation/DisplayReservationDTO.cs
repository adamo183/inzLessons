using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inzLessons.Shared.Reservation
{
    public class DisplayReservationDTO
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Student { get; set; }
        public bool IsOnline { get; set; }
        public string Description { get; set; }
    }
}
