using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inzLessons.Shared.Group
{
    public class LessonsGroupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> MembersIds { get; set; } 
        public LessonsGroupDTO() 
        {
            MembersIds = new List<int>();
        }
    }
}
