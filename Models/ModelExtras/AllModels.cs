using ExamSchedule.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSchedule.Models
{
    [HideFromSelect("Id", "ProgramCourses")]
    public partial class Programme
    {

    }

    [HideFromSelect("Id", "TeacherCourses")]
    public partial class Teacher
    {

    }

    [HideFromSelect("Id", "CourseStudents", "Exams", "ProgramCourses", "TeacherCourses", "CourseFullName", "CourseProgrammeId")]
    public partial class Course
    {
        public string CourseFullName
        {
            get
            {
                return this.ProgramCourses.ElementAt(0).Program.Name + "- " + this.Short + " - " + this.Name;
            }
        }
        [NotMapped]
        public string CourseProgrammeName
        {
            get
            {
                return this.ProgramCourses.ElementAt(0).Program.Name;
            }
        }
        [NotMapped]
        public long CourseProgrammeId
        {
            get
            {
                return this.ProgramCourses.ElementAt(0).Program.Id;
            }
        }
    }

    [HideFromSelect("Id", "CourseStudents")]
    public partial class Student
    {

    }

    [HideFromSelect("Id", "TeacherId", "CourseId", "Course", "Teacher", "Programme", "ProgrammeId")]
    public partial class TeacherCourse
    {
        [NotMapped]
        public string TeacherName { get; set; }
        [NotMapped]
        public string TeacherFullName { get; set; }
        [NotMapped]
        public string CourseName { get; set; }
        [NotMapped]
        public string CourseUKey { get; set; }
        [NotMapped]
        public string ProgrammeName { get; set; }

    }

    [HideFromSelect("Id", "ProgramId", "CourseId", "Course", "Program")]
    public partial class ProgramCourse
    {
        [NotMapped]
        public string ProgrammeName { get; set; }
        [NotMapped]
        public string CourseName { get; set; }
        [NotMapped]
        public string CourseUKey { get; set; }
    }

    [HideFromSelect("Id", "CourseId", "StudentId", "Course", "Student")]
    public partial class CourseStudent
    {
        [NotMapped]
        public string CourseName { get; set; }
        [NotMapped]
        public string CourseShortName { get; set; }
        [NotMapped]
        public string CourseUKey { get; set; }
        [NotMapped]
        public string StudentNo { get; set; }
    }


    public partial class Schedule
    {

        public string DateRange
        {
            get
            {
                return this.StartDate + " - " + this.EndDate;
            }
        }

        public string Start
        {
            get
            {

                string str = "";
                str += this.DayStartHour < 10 ? "0" + this.DayStartHour : this.DayStartHour.ToString();
                str += " : ";
                str += this.DayStartMinute < 10 ? "0" + this.DayStartMinute : this.DayStartMinute.ToString();
                return str;

            }
        }

        public string End
        {
            get
            {
                string str = "";
                str += this.DayEndHour < 10 ? "0" + this.DayEndHour : this.DayEndHour.ToString();
                str += " : ";
                str += this.DayEndMinute < 10 ? "0" + this.DayEndMinute : this.DayEndMinute.ToString();
                return str;
            }
        }

        public int DaysCount
        {
            get
            {
                var days = 0;
                DateTime s = DateTime.Parse(StartDate);
                DateTime e = DateTime.Parse(EndDate);

                days = (e - s).Days;

                return days;
            }
        }



        public DateTime StartDateParsed
        {
            get
            {
                DateTime dx = DateTime.Parse(StartDate);
                DateTime result = new DateTime(dx.Year, dx.Month, dx.Day, (int)DayStartHour, (int)DayStartMinute, 0);
                return result;
            }
        }

        public DateTime EndDateParsed
        {
            get
            {
                DateTime dx = DateTime.Parse(EndDate);
                DateTime result = new DateTime(dx.Year, dx.Month, dx.Day, (int)DayEndHour, (int)DayEndMinute, 0);
                return result;
            }
        }



        public int ExamCount { get { return this.Exams.Count; } }
        public int RestrictionCount { get { return this.ScheduleRestrictions.Count; } }
    }

    public partial class ScheduleRestriction
    {
        public string Start
        {
            get
            {
                string str = "";
                str += this.StartHour < 10 ? "0" + this.StartHour : this.StartHour.ToString();
                str += " : ";
                str += this.StartMinute < 10 ? "0" + this.StartMinute : this.StartMinute.ToString();
                return str;
            }
        }
        public string End
        {
            get
            {
                string str = "";
                str += this.EndHour < 10 ? "0" + this.EndHour : this.EndHour.ToString();
                str += " : ";
                str += this.EndMinute < 10 ? "0" + this.EndMinute : this.EndMinute.ToString();
                return str;
            }
        }

        public DateTime StartParsed
        {
            get
            {
                DateTime dx = DateTime.Parse(Date);
                DateTime result = new DateTime(dx.Year, dx.Month, dx.Day, (int)StartHour, (int)StartMinute, 0);
                return result;
            }
        }

        public DateTime EndParsed
        {
            get
            {
                DateTime dx = DateTime.Parse(Date);
                DateTime result = new DateTime(dx.Year, dx.Month, dx.Day, (int)EndHour, (int)EndMinute, 0);
                return result;
            }
        }



    }


    public partial class Exam
    {
        public string Start
        {
            get
            {
                if (this.StartHour.HasValue && this.StartMinute.HasValue)
                {

                    return this.StartHour.ToString() + " : " + this.StartMinute.ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        

        [NotMapped]
        public bool IsPinnedParsed
        {

            get
            {
                if (!IsPined.HasValue || !IsPined.Value)
                {
                    return false;
                }
                else
                {
                    return true;

                }
            }
            set
            {
                IsPinnedParsed = value;
            }
        }

        [NotMapped]
        public bool IsMergedParsed
        {
            get
            {
                if (IsMerged.HasValue || IsMerged.Value == true)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
            set { IsMergedParsed = value; }
        }


        [NotMapped]
        public long? TotalTime
        {
            get
            {
                return Duration + BeforeSpace + AfterSpace;
            }
        }

        [NotMapped]
        public int TotalStudentCount
        {
            get
            {
                var count = Course.CourseStudents.Count;
                if (IsMergedParsed)
                {
                    count = InverseMergerExam.Sum(x => x.Course.CourseStudents.Count);
                }
                return count;
            }
        }
    }


    [HideFromSelect("Id", "ExamRooms")]
    public partial class Room
    {

    }

}
