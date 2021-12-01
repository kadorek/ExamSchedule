using System;
using System.Collections.Generic;

#nullable disable

namespace ExamSchedule.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseStudents = new HashSet<CourseStudent>();
            Exams = new HashSet<Exam>();
            ProgramCourses = new HashSet<ProgramCourse>();
            TeacherCourses = new HashSet<TeacherCourse>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Ukey { get; set; }
        public string Short { get; set; }
        public long Grade { get; set; }

        public virtual ICollection<CourseStudent> CourseStudents { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<ProgramCourse> ProgramCourses { get; set; }
        public virtual ICollection<TeacherCourse> TeacherCourses { get; set; }
    }
}
