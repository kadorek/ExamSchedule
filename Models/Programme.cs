using System;
using System.Collections.Generic;

#nullable disable

namespace ExamSchedule.Models
{
    public partial class Programme
    {
        public Programme()
        {
            ProgramCourses = new HashSet<ProgramCourse>();
            TeacherCourses = new HashSet<TeacherCourse>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProgramCourse> ProgramCourses { get; set; }
        public virtual ICollection<TeacherCourse> TeacherCourses { get; set; }
    }
}
