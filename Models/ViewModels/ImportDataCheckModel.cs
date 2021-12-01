using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSchedule.Models.ViewModels
{
	[Keyless]
	public partial class ImportDataCheckModel
	{
		[NotMapped]
		public string ClassName { get; set; }

		[NotMapped]
		public List<ImportDataPropertyCheckModel> Relations { get; set; }

	}
}
