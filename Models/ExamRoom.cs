using System;
using System.Collections.Generic;

#nullable disable

namespace ExamSchedule.Models
{
    public partial class ExamRoom
    {
        public long Id { get; set; }
        public long ExamId { get; set; }
        public long RoomId { get; set; }

        public virtual Exam Exam { get; set; }
        public virtual Room Room { get; set; }
    }
}
