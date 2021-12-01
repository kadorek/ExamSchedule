using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSchedule.Models.ViewModels
{
	[Keyless]
	public class PropertySelectModel
	{
		[NotMapped]
		public int CellIndex { get; set; }

		[NotMapped]
		public List<String> PropertyNames { get; set; }

		public PropertySelectModel() {
			PropertyNames = new List<string>();
		}
	}
}
