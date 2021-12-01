using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSchedule.Models.ViewModels
{
	[Keyless]
	public class ImportDataViewModel
	{

		[NotMapped]
		[DataType(DataType.Upload)]
		public IFormFile DataFile { get; set; }

		
	}
}
