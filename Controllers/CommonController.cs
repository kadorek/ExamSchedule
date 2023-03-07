using ExamSchedule.Models;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509.Qualified;
using System.Linq;

namespace ExamSchedule.Controllers
{
    public class CommonController : Controller
    {

        private readonly examdataContext _context;

        public CommonController(examdataContext contex)
        {
            _context = contex;
        }

        [HttpPost]
        public JsonResult GetEntityListForSchedule(long type, long scheduleId)
        {
            JsonResult data = null;
            var entityType = (ComplexRestrictionTypeEnum)type;
            dynamic list = null;
            switch (entityType)
            {
                case ComplexRestrictionTypeEnum.Teacher:

                    list = _context.Teachers.Select(x => new { val = x.Id, text = $"{x.Title} {x.Name} {x.LastName}" }).ToList();
                    data = Json(list);

                    break;
                case ComplexRestrictionTypeEnum.Course:

                    list = _context.Courses.Select(x => new { val = x.Id, text = $"{x.CourseShortFullName} {x.Name}" }).ToList();
                    data = Json(list);
                    break;
                case ComplexRestrictionTypeEnum.Student:

                    list = _context.Students.Select(x => new { val = x.Id, text = $"{x.Name} {x.LastName}" });
                    data = Json(list);

                    break;
                case ComplexRestrictionTypeEnum.Programme:
                    list = _context.Programmes.Select(x => new { val = x.Id, text = $"{x.Name}" });
                    data = Json(list);
                    break;
                case ComplexRestrictionTypeEnum.Schedule:

                    list = _context.Students.Select(x => new { val = x.Id, text = $"{x.Name} {x.LastName}" });
                    data = Json(list);
                    break;
                case ComplexRestrictionTypeEnum.Exam:

                    list = _context.Exams.Where(x=>x.ScheduleId==scheduleId).Select(x => new { val = x.Id, text = $"{x.Course.CourseShortFullName}" });
                    data = Json(list);

                    break;
            }

            return data;
        }
    }
}
