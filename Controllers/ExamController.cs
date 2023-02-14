using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamSchedule.Models;
using ExamSchedule.Models.ViewModels;
using System.Runtime.InteropServices.WindowsRuntime;
using NPOI.OpenXmlFormats.Dml;

namespace ExamSchedule.Controllers
{
    public class ExamController : Controller
    {
        private readonly examdataContext _context;

        public ExamController(examdataContext context)
        {
            _context = context;
        }

        // GET: Exam
        public async Task<IActionResult> Index()
        {
            var examdataContext = _context.Exams.Include(e => e.Course).Include(e => e.MergerExam).Include(e => e.Schedule);
            return View(await examdataContext.ToListAsync());
        }

        // GET: Exam/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.Course)
                .Include(e => e.MergerExam)
                .Include(e => e.Schedule)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (exam.IsMergedParsed == true)
            {
                var examList = _context.Exams.Where(ex => ex.MergerExamId == id).ToList();
                var total = 0;

                foreach (var item in examList)
                {
                    total += item.Course.CourseStudents.Count;
                }
                ViewData["TotalStudentCount"] = total;
            }


            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // GET: Exam/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", null, "CourseProgrammeName");
            ViewData["MergerExamId"] = new SelectList(_context.Exams, "Id", "Id");
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "Title");
            ViewData["CourseId"] = ((SelectList)ViewData["CourseId"]).Prepend(new SelectListItem() { Value = "0", Text = "Lütfen Kurs Seçiniz.", Selected = true });
            ViewData["ScheduleId"] = ((SelectList)ViewData["ScheduleId"]).Prepend(new SelectListItem { Value = "0", Text = "Lütfen Takvim seçiniz.", Selected = true });

            return View();
        }

        // POST: Exam/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Exam exam)
        { //[Bind("Id,ScheduleId,CourseId,Date,IsPined,IsMerged,MergerExamId,StartHour,StartMinute,EndHour,EndMinute,Duration,BeforeSpace,AfterSpace")]         
            if (ModelState.IsValid)
            {
                _context.Add(exam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", null, "CourseProgrammeName");
            ViewData["MergerExamId"] = new SelectList(_context.Exams, "Id", "Id");
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "Title");
            ViewData["CourseId"] = ((SelectList)ViewData["CourseId"]).Prepend(new SelectListItem() { Value = "0", Text = "Lütfen Kurs Seçiniz.", Selected = true });
            ViewData["ScheduleId"] = ((SelectList)ViewData["ScheduleId"]).Prepend(new SelectListItem { Value = "0", Text = "Lütfen Takvim seçiniz.", Selected = true });


            return View(exam);
        }

        // GET: Exam/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams.FindAsync(id);
            
            if (exam == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", exam.CourseId);
            ViewData["MergerExamId"] = new SelectList(_context.Exams, "Id", "Course.CourseFullName", exam.MergerExamId);
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "Title", exam.ScheduleId);
            return View(exam);
        }

        // POST: Exam/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ScheduleId,CourseId,Date,IsPined,IsMerged,MergerExamId,StartHour,StartMinute,Duration,BeforeSpace,AfterSpace")] Exam exam)
        {
            if (id != exam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    if (!ExamExists(exam.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", exam.CourseId);
            ViewData["MergerExamId"] = new SelectList(_context.Exams, "Id", "Id", exam.MergerExamId);
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "Title", exam.ScheduleId);
            return View(exam);
        }

        // GET: Exam/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.Course)
                .Include(e => e.MergerExam)
                .Include(e => e.Schedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // POST: Exam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var exam = await _context.Exams.FindAsync(id);
            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamExists(long id)
        {
            return _context.Exams.Any(e => e.Id == id);
        }

        public async Task<JsonResult> GetScheduleDates(long sId)
        {

            Schedule s = await _context.Schedules.FindAsync(sId);
            SiteMessage m;
            if (s == null)
            {
                m = new SiteMessage("Takvim bulunamadı", MessageType.Error);
                m.Data = null;
            }
            else
            {
                m = new SiteMessage("OK", MessageType.Success);
                m.Data = new { keys = new String[] { "start", "end" }, start = s.StartDate, end = s.EndDate };
            }


            return Json(m);
        }

        public IActionResult BatchCreate()
        {

            ViewData["Courses"] = _context.Courses.ToList().GroupBy(u => u.CourseProgrammeName).Select(grp => new { Name = grp.Key, List = grp.ToList() }).ToList();
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "Title");
            ViewData["ScheduleId"] = ((SelectList)ViewData["ScheduleId"]).Prepend(new SelectListItem { Value = "0", Text = "Lütfen Takvim seçiniz.", Selected = true });


            return View();
        }
        [HttpPost, ActionName("BatchCreate")]
        public async Task<IActionResult> BatchCreate(BatchExamCreateModel becm)
        {

            if (becm.ScheduleId == "0")
            {
                return BadRequest();
            }

            var ids = becm.CourseIds.Split('-').ToList();
            foreach (var item in ids)
            {
                Exam e = new Exam();
                if (becm.Duration != "0")
                {
                    e.Duration = Convert.ToInt32(becm.Duration);
                }
                if (becm.BeforeSpace != "0")
                {
                    e.BeforeSpace = Convert.ToInt32(becm.BeforeSpace);
                }
                if (becm.AfterSpace != "0")
                {
                    e.AfterSpace = Convert.ToInt32(becm.AfterSpace);
                }
                e.CourseId = Convert.ToInt64(item);
                e.ScheduleId = Convert.ToInt64(becm.ScheduleId);

                if (_context.Exams.FirstOrDefault(x => x.CourseId == e.CourseId && x.ScheduleId == e.ScheduleId) == null)

                {
                    await _context.AddAsync(e);
                    await _context.SaveChangesAsync();
                }
            }


            return Ok("success"); ;

        }


        public async Task<String> GetExamName(long id)
        {
            var exam = await _context.Exams.FirstOrDefaultAsync(x => x.Id == id);
            if (exam == null) { return ""; }
            else
            {
                return exam.Course.Name;
            }
        }

        [HttpPost]
        public IActionResult MergeExams(long[] examIds)
        {
            var examList = _context.Exams.Where(x => examIds.Contains(x.Id)).ToList();
            if (!examList.All(x => x.ScheduleId == examList[0].ScheduleId))
            {
                return BadRequest();
            }

            try
            {
                foreach (var item in examList)
                {
                    item.MergerExamId = examList[0].Id;
                    item.IsMerged = true;
                }
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();

            }

        }

        public JsonResult GetAllExams()
        {
            return Json(_context.Exams.Select(x => new ExamDatatableModel
            {
                Id = x.Id,
                Programme = x.Course.CourseProgrammeName,
                ProgrammeId = x.Course.CourseProgrammeId,
                Course = x.Course.Name,
                CourseId = x.CourseId,
                Schedule = x.Schedule.Title,
                ScheduleId = x.ScheduleId,
                IsMerged = x.IsMerged == false ? false : true,
                IsPined = x.IsPinnedParsed,
                MergerExamId = x.MergerExamId,
                Start = x.Start,
                Date = x.Date,
                Ukey = x.Course.Ukey
            }));

        }

        [HttpPost]
        public IActionResult BacthDelete(long[] examIds)
        {
            var examList = _context.Exams.Where(x => examIds.Contains(x.Id)).ToList();
            try
            {
                _context.Exams.RemoveRange(examList);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }



    }
}
