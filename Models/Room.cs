using System;
using System.Collections.Generic;

#nullable disable

namespace ExamSchedule.Models
{
    public partial class Room
    {
        public Room()
        {
            ExamRooms = new HashSet<ExamRoom>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Short { get; set; }
        public long Capacity { get; set; }
        public long Floor { get; set; }
        public virtual ICollection<ExamRoom> ExamRooms { get; set; }
    }
}
