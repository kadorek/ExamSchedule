using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace ExamSchedule.Controllers
{
    public class ComplexRestrictionController : Controller
    {
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
            return View();
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
