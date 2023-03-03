using ExamSchedule.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace ExamSchedule.Controllers
{
    public class ComplexRestrictionController : Controller
    {
        private readonly examdataContext _context;

        public ComplexRestrictionController(examdataContext contex)
        {
            _context = contex;
        }

        // GET: ComplexRestrictionComtroller
        public ActionResult Index()
        {
            return View();
        }

        // GET: ComplexRestrictionComtroller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //[Route("ComplexRestrictionRoute")]
        // GET: ComplexRestrictionComtroller/Create
        public ActionResult Create(long id)
        {
            ViewData["scheduleId"] = id;
            ComplexRestriction cr = new ComplexRestriction();
            cr.ScheduleId = id;

            return View(cr);
        }

        // POST: ComplexRestrictionComtroller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ComplexRestrictionComtroller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ComplexRestrictionComtroller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ComplexRestrictionComtroller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ComplexRestrictionComtroller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
