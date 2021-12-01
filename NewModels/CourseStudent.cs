using System;
using System.Collections.Generic;

#nullable disable

namespace ExamSchedule.NewModels
{
    public partial class CourseStudent
    {
        public long Id { get; set; }
        public long CourseId { get; set; }
        public long StudentId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}
