using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inzLessons.Shared.AllowedReservation
{
    public class AllowedReservationDTO
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int MaxLessonTimePerStudent { get; set; }

    }
}
