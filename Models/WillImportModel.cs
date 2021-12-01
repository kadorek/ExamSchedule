using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSchedule.Models
{
	[Keyless]
	public class WillImportModel
	{
		[NotMapped]
		public string ModelName { get; set; }
		[NotMapped]
		public List<Object> List { get; set; }
		[NotMapped]
		public int ImportDataId { get; set; }
	}
}
