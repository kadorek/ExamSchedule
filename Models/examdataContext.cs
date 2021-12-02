using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ExamSchedule.Models.ViewModels;
using ExamSchedule.Models;

#nullable disable

namespace ExamSchedule.Models
{
	public partial class examdataContext : DbContext
	{
		public examdataContext()
		{
		}

		public examdataContext(DbContextOptions<examdataContext> options)
			: base(options)
		{
		}

		public virtual DbSet<Course> Courses { get; set; }
		public virtual DbSet<CourseStudent> CourseStudents { get; set; }
		public virtual DbSet<Exam> Exams { get; set; }
		public virtual DbSet<ExamRoom> ExamRooms { get; set; }
		public virtual DbSet<ProgramCourse> ProgramCourses { get; set; }
		public virtual DbSet<Programme> Programmes { get; set; }
		public virtual DbSet<Room> Rooms { get; set; }

		public virtual DbSet<Schedule> Schedules { get; set; }
		public virtual DbSet<Student> Students { get; set; }
		public virtual DbSet<Teacher> Teachers { get; set; }
		public virtual DbSet<TeacherCourse> TeacherCourses { get; set; }
		public virtual DbSet<ImportedData> ImportedDatas { get; set; }
		public virtual DbSet<ScheduleRestriction> ScheduleRestrictions { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
				optionsBuilder.UseSqlite("DataSource=.\\Data\\exam-data.db");
				optionsBuilder.UseLazyLoadingProxies();
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Course>(entity =>
			{
				entity.ToTable("Course");

				entity.HasIndex(e => e.Id, "IX_Course_ID")
					.IsUnique();

				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.Name).IsRequired();

				entity.Property(e => e.Ukey).HasColumnName("UKey");
				
			});

			modelBuilder.Entity<CourseStudent>(entity =>
			{
				entity.ToTable("CourseStudent");

				entity.HasIndex(e => e.Id, "IX_CourseStudent_ID")
					.IsUnique();

				entity.Property(e => e.Id).HasColumnName("ID");

				entity.HasOne(d => d.Course)
					.WithMany(p => p.CourseStudents)
					.HasForeignKey(d => d.CourseId)
					.OnDelete(DeleteBehavior.ClientSetNull);

				entity.HasOne(d => d.Student)
					.WithMany(p => p.CourseStudents)
					.HasForeignKey(d => d.StudentId)
					.OnDelete(DeleteBehavior.ClientSetNull);
			});

			modelBuilder.Entity<Exam>(entity =>
			{
				entity.ToTable("Exam");

				entity.HasIndex(e => e.Id, "IX_Exam_ID")
					.IsUnique();

				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.AfterSpace).HasDefaultValueSql("0");

				entity.Property(e => e.BeforeSpace).HasDefaultValueSql("0");

				entity.Property(e => e.IsMerged).HasDefaultValueSql("0");

				entity.Property(e => e.IsPined).HasDefaultValueSql("0");

				entity.HasOne(d => d.Course)
					.WithMany(p => p.Exams)
					.HasForeignKey(d => d.CourseId)
					.OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(d => d.MergerExam)
					.WithMany(p => p.InverseMergerExam)
					.HasForeignKey(d => d.MergerExamId);

				entity.HasOne(d => d.Schedule)
					.WithMany(p => p.Exams)
					.HasForeignKey(d => d.ScheduleId)
					.OnDelete(DeleteBehavior.Cascade);
			});

			modelBuilder.Entity<ExamRoom>(entity =>
			{
				entity.ToTable("ExamRoom");

				entity.HasIndex(e => e.Id, "IX_ExamRoom_ID")
					.IsUnique();

				entity.Property(e => e.Id).HasColumnName("ID");

				entity.HasOne(d => d.Exam)
					.WithMany(p => p.ExamRooms)
					.HasForeignKey(d => d.ExamId)
					.OnDelete(DeleteBehavior.ClientSetNull);

				entity.HasOne(d => d.Room)
					.WithMany(p => p.ExamRooms)
					.HasForeignKey(d => d.RoomId)
					.OnDelete(DeleteBehavior.ClientSetNull);
			});

			modelBuilder.Entity<ProgramCourse>(entity =>
			{
				entity.ToTable("ProgramCourse");

				entity.HasIndex(e => e.Id, "IX_ProgramCourse_ID")
					.IsUnique();

				entity.Property(e => e.Id).HasColumnName("ID");

				entity.HasOne(d => d.Course)
					.WithMany(p => p.ProgramCourses)
					.HasForeignKey(d => d.CourseId)
					.OnDelete(DeleteBehavior.ClientSetNull);

				entity.HasOne(d => d.Program)
					.WithMany(p => p.ProgramCourses)
					.HasForeignKey(d => d.ProgramId)
					.OnDelete(DeleteBehavior.ClientSetNull);
			});

			modelBuilder.Entity<Programme>(entity =>
			{
				entity.ToTable("Programme");

				entity.HasIndex(e => e.Id, "IX_Programme_ID")
					.IsUnique();

				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.Name).IsRequired();
			});

			modelBuilder.Entity<Room>(entity =>
			{
				entity.ToTable("Room");

				entity.HasIndex(e => e.Id, "IX_Room_ID")
					.IsUnique();

				entity.HasIndex(e => e.Short, "IX_Room_Short")
					.IsUnique();

				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.Name).IsRequired();

				entity.Property(e => e.Short).IsRequired();
			});

			modelBuilder.Entity<Schedule>(entity =>
			{
				entity.ToTable("Schedule");

				entity.HasIndex(e => e.Id, "IX_Schedule_ID")
					.IsUnique();

				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.EndDate).IsRequired();

				entity.Property(e => e.StartDate).IsRequired();

				entity.Property(e => e.Title).IsRequired();

				//entity.HasMany(d => d.ScheduleRestrictions)
				//		.WithOne(p => p.Schedule)
				//		.OnDelete(DeleteBehavior.ClientCascade);

			});

			modelBuilder.Entity<ScheduleRestriction>(entity =>
			{
				entity.ToTable("ScheduleRestriction");

				entity.HasIndex(e => e.Id, "IX_ScheduleRestriction_ID")
					.IsUnique();

				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.Date).IsRequired();

				entity.HasOne(d => d.Schedule)
					.WithMany(p => p.ScheduleRestrictions)
					.HasForeignKey(d => d.ScheduleId)
					.OnDelete(DeleteBehavior.Cascade);
			});

			modelBuilder.Entity<Student>(entity =>
			{
				entity.ToTable("Student");

				entity.HasIndex(e => e.Id, "IX_Student_ID")
					.IsUnique();

				entity.HasIndex(e => e.StudentNo, "IX_Student_StudentNo")
					.IsUnique();

				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.Name).IsRequired();

				entity.Property(e => e.LastName).IsRequired();
			});

			modelBuilder.Entity<Teacher>(entity =>
			{
				entity.ToTable("Teacher");

				entity.HasIndex(e => e.Id, "IX_Teacher_ID")
					.IsUnique();

				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.Name).IsRequired();

				entity.Property(e => e.LastName).IsRequired();
			});

			modelBuilder.Entity<TeacherCourse>(entity =>
			{
				entity.ToTable("TeacherCourse");

				entity.HasIndex(e => e.Id, "IX_TeacherCourse_ID")
					.IsUnique();

				entity.Property(e => e.Id).HasColumnName("ID");

				entity.HasOne(d => d.Course)
					.WithMany(p => p.TeacherCourses)
					.HasForeignKey(d => d.CourseId)
					.OnDelete(DeleteBehavior.ClientSetNull);

				entity.HasOne(d => d.Teacher)
					.WithMany(p => p.TeacherCourses)
					.HasForeignKey(d => d.TeacherId)
					.OnDelete(DeleteBehavior.ClientSetNull);

				entity.HasOne(d => d.Programme)
					.WithMany(p => p.TeacherCourses)
					.HasForeignKey(d => d.ProgrammeId)
					.OnDelete(DeleteBehavior.ClientSetNull);
			});

			modelBuilder.Entity<ImportedData>(entity =>
			{
				entity.ToTable("ImportedData");
				entity.HasIndex(e => e.Id, "IX_ImportedData_ID").IsUnique(); ;
				entity.Property(e => e.Id).HasColumnName("ID");
				entity.Property(e => e.Date).IsRequired();
				entity.Property(e => e.Data).IsRequired();
				entity.Property(e => e.MIMEType).IsRequired();

			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

		public DbSet<ExamSchedule.Models.ViewModels.ImportDataViewModel> ImportDataViewModel { get; set; }

		public DbSet<ExamSchedule.Models.ScheduleRestriction> ScheduleRestriction { get; set; }
	}
}
