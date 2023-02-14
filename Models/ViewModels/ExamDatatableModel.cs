namespace ExamSchedule.Models.ViewModels
{
    public class ExamDatatableModel
    {
        public long Id { get; set; }
        public string Schedule { get; set; }
        public long ScheduleId { get; set; }
        public long CourseId { get; set; }
        public string Course { get; set; }
        public string Programme { get; set; }
        public long ProgrammeId { get; set; }
        public bool? IsPined { get; set; }
        public bool? IsMerged { get; set; }
        public long? MergerExamId { get; set; }
        public string Start { get; set; }
        public string Date { get; set; }
        public string Ukey { get; set; }
    }
}
