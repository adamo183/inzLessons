using inzLessons.Shared.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inzLessons.Shared.Reservation
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
        public List<int> UserIds { get; set; }
        public int GroupId { get; set; }
        public bool IsOnline { get; set; }
        public List<UserDTO> Students { get; set; }
        public ReservationDTO() { }
    }
}
