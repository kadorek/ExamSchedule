using System;
using System.Collections.Generic;

#nullable disable

namespace ExamSchedule.Models
{
	public partial class TeacherCourse
	{
		public long Id { get; set; }
		public long TeacherId { get; set; }
		public long CourseId { get; set; }

		public long ProgrammeId { get; set; }

		public virtual Course Course { get; set; }
		public virtual Teacher Teacher { get; set; }
		public virtual Programme Programme { get; set; }
	}
}
