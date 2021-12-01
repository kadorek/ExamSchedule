using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSchedule.Models.ViewModels
{
	

	[Keyless]
	public class ImportDataPropertyCheckModel
	{

		[NotMapped]
		public string Name { get; set; }
		[NotMapped]
		public int ColIndex { get; set; }
		[NotMapped]
		public bool IsUnique { get; set; }

	}
}
