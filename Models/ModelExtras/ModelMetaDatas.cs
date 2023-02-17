using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExamSchedule.Models.ModelExtras
{
    public class ScheduleMetaData
    {
        [JsonIgnore]
        public virtual ICollection<Exam> Exams { get; set; }
        [JsonIgnore]
        public virtual ICollection<ScheduleRestriction> ScheduleRestrictions { get; set; }
    }
}
