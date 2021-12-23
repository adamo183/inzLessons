using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inzLessons.Shared.ReservationRequest
{
    public class ReservationRequestDTO
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TeacherId { get; set; }
        public int UserId { get; set; }
        public string TeacherName { get; set; }
        public string StudentName { get; set; }
        public int AllowedReservationId { get; set; }
        public string Description { get; set; }
    }
}
