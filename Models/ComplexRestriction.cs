namespace ExamSchedule.Models
{
    public class ComplexRestriction
    {
        public virtual Schedule Schedule { get; set; }
        public long ScheduleId { get; set; }
        public long Id { get; set; }
        public long Type { get; set; }
        public long EntityId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
    }
}

