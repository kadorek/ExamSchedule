using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamSchedule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamSchedule.Controllers
{
	public class ScheduleController : Controller
	{
		private readonly examdataContext _context;

		public ScheduleController(examdataContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _context.Schedules.ToListAsync());
		}

		public IActionResult Create()
		{

			return View(new Schedule());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Schedule sch)
		{
			if (ModelState.IsValid)
			{
				await _context.AddAsync(sch);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			return View(sch);

		}

		public async Task<IActionResult> AddRestriction(long id)
		{
			Schedule _s = null;
			try
			{
				_s = await _context.Schedules.FindAsync(id);
				if (_s == null)
				{
					throw new Exception("Schedule not found");
				}
			}
			catch (Exception ex)
			{
				ViewData["Message"] = ex;
				return RedirectToAction("Index");
			}


			ScheduleRestriction sr = new ScheduleRestriction();
			sr.ScheduleId = _s.Id;
			sr.Schedule = _s;

			return View(sr);
		}

		[HttpPost]
		public async Task<IActionResult> AddRestriction([FromForm] ScheduleRestriction sr)
		{

			if (!ModelState.IsValid)
			{
				return View(sr);
			}

			await _context.AddAsync(sr);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Details(long? id)
		{

			var _s = await _context.FindAsync(typeof(Schedule), id);//  _context.Schedules.Include("ScheduleRestrictions").FirstOrDefaultAsync(x=>x.Id==id);

			return View(_s);

		}

		public async Task<IActionResult> RestrictionList(long? id)
		{

			var _s = await _context.FindAsync(typeof(Schedule), id);
			ViewData["ScheduleId"] = id;
			return View(((Schedule)_s).ScheduleRestrictions);

		}


		public async Task<IActionResult> RestrictionDelete(long? id)
		{

			if (id == null)
			{
				return NotFound();
			}

			var sr = await _context.ScheduleRestrictions
				.FirstOrDefaultAsync(m => m.Id == id);

			if (sr == null)
			{
				return NotFound();
			}

			return View(sr);
		}

		[HttpPost, ActionName("RestrictionDelete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> RestrictionDeleteConfirmed(long id)
		{
			var sr = await _context.ScheduleRestrictions.FindAsync(id);
			_context.ScheduleRestrictions.Remove(sr);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}


		public async Task<IActionResult> RestrictionEdit(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var sr = await _context.ScheduleRestrictions.FindAsync(id);
			if (sr == null)
			{
				return NotFound();
			}
			return View(sr);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> RestrictionEdit(long id, ScheduleRestriction sr)
		{
			if (id != sr.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(sr);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!RestrictionExist(sr.Id))
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
			return View(sr);
		}


		public async Task<IActionResult> Delete(long? id)
		{

			if (id == null)
			{
				return NotFound();
			}

			var s = await _context.Schedules.FindAsync(id);
			if (s == null)
			{
				return NotFound();
			}

			return View(s);

		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(long? id)
		{
			var s = await _context.Schedules.Include(x => x.ScheduleRestrictions).FirstOrDefaultAsync(x => x.Id == id);
			//
			_context.Schedules.Remove(s);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));

		}


		public async Task<IActionResult> Edit(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var s = await _context.Schedules.FindAsync(id);
			if (s == null)
			{
				return NotFound();
			}
			return View(s);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(long id, Schedule s)
		{
			if (id != s.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(s);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ScheduleExist(s.Id))
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
			return View(s);
		}



		public async Task<IActionResult> AddExam(long id)
		{

			Schedule _s = null;
			try
			{
				_s = await _context.Schedules.FindAsync(id);
				if (_s == null)
				{
					throw new Exception("Schedule not found");
				}
			}
			catch (Exception ex)
			{
				ViewData["Message"] = ex;
				return RedirectToAction("Index");
			}

			Exam _exam = new Exam();
			_exam.ScheduleId = _s.Id;
			_exam.Schedule = _s;

			ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", null, "CourseProgrammeName");
			ViewData["CourseId"] = ((SelectList)ViewData["CourseId"]).Prepend(new SelectListItem() { Value = "0", Text = "Lütfen Kurs Seçiniz.", Selected = true });

			return View(_exam);

		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddExam(long id,[FromForm] Exam e)
		{
			//Schedule _s = null;
			//try
			//{
			//	_s = await _context.Schedules.FindAsync(id);
			//	if (_s == null)
			//	{
			//		throw new Exception("Schedule not found");
			//	}
			//}
			//catch (Exception ex)
			//{
			//	ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", null, "CourseProgrammeName");
			//	ViewData["CourseId"] = ((SelectList)ViewData["CourseId"]).Prepend(new SelectListItem() { Value = "0", Text = "Lütfen Kurs Seçiniz.", Selected = true });
			//	ViewData["Message"] = ex;
			//	return RedirectToAction("AddExam", new { id = id });
			//}

			try
			{
				if (ModelState.IsValid)
				{
					_context.Add(e);
					await _context.SaveChangesAsync();
				}
				else
				{
					throw new Exception("ModelState is invalid.");
				}
			}
			catch (Exception ex)
			{
				ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", null, "CourseProgrammeName");
				ViewData["CourseId"] = ((SelectList)ViewData["CourseId"]).Prepend(new SelectListItem() { Value = "0", Text = "Lütfen Kurs Seçiniz.", Selected = true });
				ViewData["Message"] = ex.Message;
				return RedirectToAction("AddExam", new { id = id });
			}

			return RedirectToAction("Index");



		}




		private bool RestrictionExist(long id)
		{

			return _context.ScheduleRestrictions.Any(x => x.Id == id);
		}


		private bool ScheduleExist(long id)
		{
			return _context.Schedules.Any(x => x.Id == id);
		}


	}
}
