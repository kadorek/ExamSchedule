using System;
using System.Collections.Generic;

#nullable disable

namespace ExamSchedule.Models
{
    public partial class ProgramCourse
    {
        public long Id { get; set; }
        public long ProgramId { get; set; }
        public long CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Programme Program { get; set; }
    }
}
