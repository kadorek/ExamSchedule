using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ExamSchedule.Models.ArrangmentModels
{


    [NotMapped]
    public class DayPart
    {
        public string UniqueKey { get; set; }
        public int IndexDay { get; set; }
        public int IndexPart { get; set; }
        public bool IsRestricted { get; set; }
        public override string ToString()
        {
            var str = "";
            str = IsRestricted ? "R" : "N";
            return str;
        }


    }

    [NotMapped]
    public class ArrangementSettings
    {
        public int MaxExamCountPerDay { get; set; } = 4;
        public int DefaultExamPartCount { get; set; } = 3;
        public int DefaultDayPartDuration { get; set; } = 30;

    }

    [NotMapped]
    public class MainArrangementModel
    {
        public Schedule Schedule { get; set; }
        public ArrangementSettings Settings { get; set; }
        public List<DayPart> Parts { get; set; }
        public List<ExamPlacement> ExamPlacements { get; set; }
        public MainArrangementModel()
        {
            Settings = new ArrangementSettings();
            Parts = new List<DayPart>();
        }

    }

    [NotMapped]
    public class ExamPlacement
    {
        public long ExamId { get; set; }
        public List<Room> Rooms { get; set; }
        public List<string> DayPartUniqueKeys { get; set; }
    }
}
