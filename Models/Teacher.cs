using System;
using System.Collections.Generic;

#nullable disable

namespace ExamSchedule.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            TeacherCourses = new HashSet<TeacherCourse>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
		public string Title { get; set; }

		public virtual ICollection<TeacherCourse> TeacherCourses { get; set; }
    }
}
