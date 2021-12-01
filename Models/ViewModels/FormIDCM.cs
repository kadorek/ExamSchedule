using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSchedule.Models.ViewModels
{
	[Keyless]
	public class FormIDCM
	{
		[NotMapped]
		public long? ImportedDataId { get; set; }

		[NotMapped]
		public ImportDataCheckModel Selection { get; set; }
	}
}
