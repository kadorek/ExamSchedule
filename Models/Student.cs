using System;
using System.Collections.Generic;

#nullable disable

namespace ExamSchedule.Models
{
    public partial class Student
    {
        public Student()
        {
            CourseStudents = new HashSet<CourseStudent>();
        }

        public long Id { get; set; }
        public string StudentNo { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<CourseStudent> CourseStudents { get; set; }
    }
}
