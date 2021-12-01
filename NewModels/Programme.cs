using System;
using System.Collections.Generic;

#nullable disable

namespace ExamSchedule.NewModels
{
    public partial class Programme
    {
        public Programme()
        {
            ProgramCourses = new HashSet<ProgramCourse>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProgramCourse> ProgramCourses { get; set; }
    }
}
