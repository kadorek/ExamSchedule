using System;
using System.Collections.Generic;

#nullable disable

namespace ExamSchedule.NewModels
{
    public partial class Schedule
    {
        public Schedule()
        {
            Exams = new HashSet<Exam>();
            ScheduleRestrictions = new HashSet<ScheduleRestriction>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public long DayStartHour { get; set; }
        public long DayStartMinute { get; set; }
        public long DayEndHour { get; set; }
        public long DayEndMinute { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<ScheduleRestriction> ScheduleRestrictions { get; set; }
    }
}
