using ExamSchedule.Models;
using ExamSchedule.Models.ArrangmentModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace ExamSchedule.Controllers
{
    public class ArrangeController : Controller
    {

        private readonly examdataContext _context;


        private MainArrangementModel _mam;
        public ArrangeController(examdataContext context)
        {
            _context = context;
            _mam = new MainArrangementModel();
        }

        public async Task<IActionResult> Index(long? id)
        {

            _mam.Schedule = _context.Schedules.FirstOrDefault(x => x.Id == id);
            if (_mam.Schedule == null)
            {
                throw new Exception("Schedule null");
            }
            AllArrangements();

            return View(_mam);
        }

        private void AllArrangements()
        {
            DesingModelParts();
            PlaceRestriction();
            PlacePinnedExams();

        }


        private void DesingModelParts()
        {
            Random r = new Random();
            int countDayPartPerDay = (int)((_mam.Schedule.DayEndHour - _mam.Schedule.DayStartHour) * (60 / _mam.Settings.DefaultDayPartDuration) + (_mam.Schedule.DayEndMinute - _mam.Schedule.DayStartMinute) / _mam.Settings.DefaultDayPartDuration);

            for (int i = 0; i < _mam.Schedule.DaysCount; i++)
            {
                for (int j = 0; j < countDayPartPerDay; j++)
                {
                    var dpdate = _mam.Schedule.StartDateParsed.AddDays(i).AddMinutes(j * _mam.Settings.DefaultDayPartDuration);
                    DayPart dp = new DayPart(dpdate);
                    dp.IndexDay = i;
                    dp.IndexPart = j;
                    dp.IsRestricted = false;
                    do
                    {
                        dp.UniqueKey = r.Next().ToString("x");
                    } while (_mam.Parts.Any(x => x.UniqueKey == dp.UniqueKey));
                    _mam.Parts.Add(dp);
                }
            }
        }

        private void PlaceRestriction()
        {

            var listRestrictions = _context.ScheduleRestrictions.Where(x => x.ScheduleId == _mam.Schedule.Id).ToList();

            foreach (var item in listRestrictions)
            {
                int indexDay = (DateTime.Parse(item.Date) - DateTime.Parse(_mam.Schedule.StartDate)).Days;

                int countDayPart = (int)Math.Ceiling((item.EndParsed - item.StartParsed).TotalMinutes / _mam.Settings.DefaultDayPartDuration);
                var targetDayStart = new DateTime(_mam.Schedule.StartDateParsed.AddDays(indexDay).Year, _mam.Schedule.StartDateParsed.AddDays(indexDay).Month, _mam.Schedule.StartDateParsed.AddDays(indexDay).Day, _mam.Schedule.StartDateParsed.Hour, _mam.Schedule.StartDateParsed.Minute, 0);
                int indexPart = (int)Math.Floor((item.StartParsed - targetDayStart).TotalMinutes / _mam.Settings.DefaultDayPartDuration);
                for (int i = 0; i < countDayPart; i++)
                {
                    DayPart dp = _mam.Parts.FirstOrDefault(x => x.IndexDay == indexDay && x.IndexPart == indexPart + i);
                    if (dp != null)
                    {
                        dp.IsRestricted = true;
                    }
                }
            }

        }


        private void PlacePinnedExams()
        {
            var listPinnedExams = _context.Exams.AsEnumerable().Where(x => x.ScheduleId == _mam.Schedule.Id && x.IsPinnedParsed && (!x.IsMergedParsed || x.MergerExamId == x.Id));

            foreach (var item in listPinnedExams)
            {
                var ep = new ExamPlacement();

                var allRooms = _context.Rooms.ToList();
                var exam = _context.Exams.FirstOrDefault(x => x.Id == item.Id);
                if (exam == null)
                {
                    //throws error
                }
                ep.ExamId = item.Id;
                ep.ExamName = item.Course.Name;

                var examDate = DateTime.Parse(exam.Date);
                int indexDayPart = (DateTime.Parse(exam.Date) - DateTime.Parse(_mam.Schedule.StartDate)).Days;
                int countDayPart = (int)Math.Ceiling((decimal)exam.TotalTime.Value / _mam.Settings.DefaultDayPartDuration);

                var startDx = new DateTime(examDate.Year, examDate.Month, examDate.Day, (int)exam.StartHour.Value, (int)exam.StartMinute.Value, 0);
                var targetDayStart = new DateTime(examDate.Year, examDate.Month, examDate.Day, _mam.Schedule.StartDateParsed.Hour, _mam.Schedule.StartDateParsed.Minute, 0);
                int indexPart = (int)Math.Floor((startDx - targetDayStart).TotalMinutes / _mam.Settings.DefaultDayPartDuration);


                for (int i = 0; i < countDayPart; i++)
                {
                    var dp = _mam.Parts.FirstOrDefault(x => x.IndexDay == indexDayPart && x.IndexPart == indexPart + i);
                    if (dp == null)
                    {
                        i--;
                        continue;
                    }
                    ep.DayPartUniqueKeys.Add(dp.UniqueKey);
                }


                var placementInstersection = _mam.ExamPlacements.Where(x => x.DayPartUniqueKeys.Intersect(ep.DayPartUniqueKeys).Count() > 0).ToList();

                var unproperRooms = _context.Rooms.AsEnumerable().Where(x => placementInstersection.Any(a => a.Rooms.AsEnumerable().Select(b => b.Id).Contains(x.Id))).Select(x => x.Id).ToList();

                ep.Rooms = _context.Rooms.AsEnumerable().Where(x => GetProperRooms(exam.TotalStudentCount, unproperRooms).Contains(x.Id)).ToList();

                _mam.ExamPlacements.Add(ep);
            }
        }

        private void PlaceOtherExams()
        {
            var notPinnedExams = _context.Exams.AsEnumerable().Where(x => !x.IsPinnedParsed).ToList();
            notPinnedExams = notPinnedExams.Where(x=>!x.IsMergedParsed || (x.IsMergedParsed && x.MergerExamId==x.Id)).ToList();
            var ids = notPinnedExams.Select(x => x.Id).ToList();

            foreach (var item in ids)
            {
                var ep = new ExamPlacement();
                ep.ExamId = item;
                var exam = _context.Exams.FirstOrDefault(x=>x.Id==item);
                if (exam==null)
                {

                }
                ep.ExamName = exam.Course.Name;
                int countDayPart= (int)Math.Ceiling((decimal)exam.TotalTime.Value / _mam.Settings.DefaultDayPartDuration);



            }


        }


        private List<long> GetProperRooms(int countStudent, List<long> roomUnproper)
        {
            var result = new List<long>();
            var allRooms = _context.Rooms.OrderBy(x => x.Capacity).ToList();
            foreach (var item in allRooms)
            {
                if (roomUnproper.Contains(item.Id))
                {
                    continue;
                }
                var rx = allRooms.Where(x => result.Contains(x.Id)).ToList();
                if (rx.Sum(x => x.Capacity) < countStudent + 10)
                {
                    result.Add(item.Id);
                }
            }
            return result;
        }







        [HttpPost]
        public IActionResult GetExamData(long idExam)
        {

            var result = new List<string>();

            var exam = _context.Exams.FirstOrDefault(x => x.Id == idExam);
            if (exam != null)
            {
                if (exam.IsMergedParsed)
                {
                    exam = exam.MergerExam;
                    foreach (var item in exam.InverseMergerExam)
                    {
                        result.Add(item.Course.CourseFullName);
                    }
                }
                else
                {
                    result.Add(exam.Course.CourseFullName);
                }
            }
            else
            {
                result.Add("exam is null");
            }

            return Json(result);
        }




    }
}
