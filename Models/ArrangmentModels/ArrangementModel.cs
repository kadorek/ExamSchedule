using NPOI.OpenXmlFormats.Dml.Chart;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ExamSchedule.Models.ArrangmentModels
{


    [NotMapped]
    public class DayPart
    {
        private DateTime _date;
        public string UniqueKey { get; set; }
        public int IndexDay { get; set; }
        public int IndexPart { get; set; }
        public bool IsRestricted { get; set; }
        public DateTime PartDateTime { get { return _date; } }

        public DayPart(DateTime _d)
        {
            _date = _d;
        }
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
        public List<ExamPlacement> ExamPlacements { get; set; } = new List<ExamPlacement>();
        public int MaxDayPartPerDay { get; set; } = 0;
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
        public string ExamName { get; set; }
        public List<long> Rooms { get; set; }
        public List<string> DayPartUniqueKeys { get; set; } = new List<string>();
    }
}
