using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamSchedule.Models;

namespace ExamSchedule.Controllers
{
	public class CourseController : Controller
	{
		private readonly examdataContext _context;

		public CourseController(examdataContext context)
		{
			_context = context;
		}

		// GET: Course
		public async Task<IActionResult> Index()
		{
			return View(await _context.Courses.ToListAsync());
		}

		// GET: Course/Details/5
		public async Task<IActionResult> Details(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var course = await _context.Courses
				.FirstOrDefaultAsync(m => m.Id == id);
			if (course == null)
			{
				return NotFound();
			}

			return View(course);
		}

		// GET: Course/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Course/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Ukey,Short,Grade")] Course course)
		{
			if (ModelState.IsValid)
			{
				_context.Add(course);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(course);
		}

		// GET: Course/Edit/5
		public async Task<IActionResult> Edit(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var course = await _context.Courses.FindAsync(id);
			if (course == null)
			{
				return NotFound();
			}
			return View(course);
		}

		// POST: Course/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Ukey,Short,Grade")] Course course)
		{
			if (id != course.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(course);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!CourseExists(course.Id))
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
			return View(course);
		}

		// GET: Course/Delete/5
		public async Task<IActionResult> Delete(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var course = await _context.Courses
				.FirstOrDefaultAsync(m => m.Id == id);
			if (course == null)
			{
				return NotFound();
			}

			return View(course);
		}

		// POST: Course/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(long id)
		{
			var course = await _context.Courses.FindAsync(id);
			_context.Courses.Remove(course);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool CourseExists(long id)
		{
			return _context.Courses.Any(e => e.Id == id);
		}

		[HttpPost]
		public async Task<JsonResult> GetAllCourses()
		{
			var courseList = await _context.Courses.ToListAsync();

			var rList = courseList.Select(x=>new { CourseName=x.Name,CourseId=x.Id, ProgrammeName=x.CourseProgrammeName }).OrderBy(x=>x.ProgrammeName);


			return Json(rList);
		}


	}
}
