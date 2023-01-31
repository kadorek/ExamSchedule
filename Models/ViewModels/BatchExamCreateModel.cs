namespace ExamSchedule.Models.ViewModels
{
    public class BatchExamCreateModel
    {
        public string ScheduleId { get; set; }
        public string Duration { get; set; }
        public string BeforeSpace { get; set; }
        public string AfterSpace { get; set; }
        public string CourseIds { get; set; }
    }
}
