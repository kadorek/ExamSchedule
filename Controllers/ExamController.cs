using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamSchedule.Models;
using ExamSchedule.Models.ViewModels;

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
			ViewData["MergerExamId"] = new SelectList(_context.Exams, "Id", "Id", exam.MergerExamId);
			ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "EndDate", exam.ScheduleId);
			return View(exam);
		}

		// POST: Exam/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(long id, [Bind("Id,ScheduleId,CourseId,Date,IsPined,IsMerged,MergerExamId,StartHour,StartMinute,EndHour,EndMinute,Duration,BeforeSpace,AfterSpace")] Exam exam)
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
				catch (DbUpdateConcurrencyException)
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
			ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "EndDate", exam.ScheduleId);
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
				m = new SiteMessage("Takvim bulunamadı",MessageType.Error );
				m.Data = null;
			}
			else
			{
				m = new SiteMessage("OK",MessageType.Success);
				m.Data = new { keys =new String[]{ "start","end"} , start = s.StartDate, end = s.EndDate };
			}


			return Json(m);
		}

	}
}
