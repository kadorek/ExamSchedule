using System;
using System.Collections.Generic;

#nullable disable

namespace ExamSchedule.Models
{
    public partial class ScheduleRestriction
    {
        public long Id { get; set; }
        public long ScheduleId { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public long StartHour { get; set; }
        public long StartMinute { get; set; }
        public long EndHour { get; set; }
        public long EndMinute { get; set; }

        public virtual Schedule Schedule { get; set; }
    }
}
