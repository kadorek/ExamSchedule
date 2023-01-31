using System;
using System.Collections.Generic;

#nullable disable

namespace ExamSchedule.Models
{
    public partial class Exam
    {
        public Exam()
        {
            ExamRooms = new HashSet<ExamRoom>();
            InverseMergerExam = new HashSet<Exam>();
        }

        public long Id { get; set; }
        public long ScheduleId { get; set; }
        public long CourseId { get; set; }
        public string Date { get; set; }
        public bool? IsPined { get; set; }
        public long? IsMerged { get; set; }
        public long? MergerExamId { get; set; }
        public long? StartHour { get; set; }
        public long? StartMinute { get; set; }
        public long? EndHour { get; set; }
        public long? EndMinute { get; set; }
        public long? Duration { get; set; }
        public long? BeforeSpace { get; set; }
        public long? AfterSpace { get; set; }

        public virtual Course Course { get; set; }
        public virtual Exam MergerExam { get; set; }
        public virtual Schedule Schedule { get; set; }
        public virtual ICollection<ExamRoom> ExamRooms { get; set; }
        public virtual ICollection<Exam> InverseMergerExam { get; set; }
    }
}
