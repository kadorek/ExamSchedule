using ExamSchedule.Models;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509.Qualified;

namespace ExamSchedule.Controllers
{
    public class CommonController : Controller
    {

        private readonly examdataContext _context;

        public CommonController(examdataContext contex)
        {
            _context = contex;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        public JsonResult GetEntityListForSchedule(long type, long scheduleId)
        {
            var entityType = (ComplexRestrictionTypeEnum)type;
            var typeName = "";
            //switch (entityType)
            //{
            //    case ComplexRestrictionTypeEnum.Teacher:
            //        break;
            //    case ComplexRestrictionTypeEnum.Course:
            //        break;
            //    case ComplexRestrictionTypeEnum.Student:
            //        break;
            //    case ComplexRestrictionTypeEnum.Programme:
            //        break;
            //    case ComplexRestrictionTypeEnum.Schedule:
            //        break;
            //    case ComplexRestrictionTypeEnum.Exam:
            //        break;
            //    default:
            //        break;
            //}
            ///var list = _context.Set(entityType.ToString()).ToList();



            return null;// Json(list);
        }
    }
}
