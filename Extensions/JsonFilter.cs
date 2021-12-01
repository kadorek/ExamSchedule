using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSchedule.Extensions
{
    public class JsonFilter : ActionFilterAttribute
    {
        public string Param { get; set; }
        public Type JsonDataType { get; set; }
        public override async void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.ContentType.Contains("application/json"))
            {
                string inputContent;
                //filterContext.HttpContext.Request.Body.
                using (var sr = new StreamReader(filterContext.HttpContext.Request.Body))
                {
                    inputContent =await  sr.ReadToEndAsync();
                }
                var result = JsonConvert.DeserializeObject(inputContent,JsonDataType);


                filterContext.ActionArguments[Param] = result;
            }
        }


        



	}
}
