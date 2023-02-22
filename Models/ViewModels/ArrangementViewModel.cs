namespace ExamSchedule.Models.ViewModels
{
    public class ArrangementViewModel
    {

        public long Id { get; set; }
        public string ScheduleName { get; set; }
        public int TotalExamCount { get; set; }
        public int PlacedExamCount { get; set; }
    }
}
