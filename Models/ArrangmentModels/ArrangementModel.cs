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
            str = UniqueKey + "-" + PartDateTime.ToString() + " " + (IsRestricted ? "R" : "N");
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

        //private static MainArrangementModel _object;

        //public static MainArrangementModel Object
        //{
        //    get
        //    {
        //        if (_object == null)
        //        {
        //            _object = new MainArrangementModel();
        //        }
        //        return _object;

        //    }
        //}
        public Schedule Schedule { get; set; }
        public long ScheduleId { get; set; } = -1;
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
        private readonly string _unique;
        private bool isDebug = false;
        public long ExamId { get; set; }
        public string ExamName { get; set; }
        public string ShortExamName { get; set; }
        public DateTime? ExamFullDate { get; set; }
        public List<long> Rooms { get; set; }
        public List<string> DayPartUniqueKeys { get; set; } = new List<string>();

        public String UniqueKey { get { return _unique; } }
        public ExamPlacement()
        {
            _unique = Guid.NewGuid().ToString();
        }
        public ExamPlacement(bool _isDebug)
        {
            isDebug = _isDebug;
            _unique = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            var str = "";
            str = ExamId + " - " + ShortExamName;
            if (isDebug && ExamFullDate.HasValue)
            {
                str += "- " + ExamFullDate.ToString();
            }
            return str;
        }
    }

    [NotMapped]
    public class ExamPlacementViewModel
    {
        public string UniqueKey { get; set; }
        public List<Room> Rooms { get; set; }
        public DateTime ExamFullDate { get; set; }
        public Exam Exam { get; set; }
    }
}
