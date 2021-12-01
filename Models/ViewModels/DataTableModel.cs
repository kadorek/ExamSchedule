using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSchedule.Models.ViewModels
{
	public class DataTableModel
	{

		public List<String> TableHeaders { get; set; }
		public List<List<String>> ValuesAsRows{ get; set; }
		public long ImportedDataId { get; set; }
		public DataTableModel() {
			TableHeaders = new List<string>();
			ValuesAsRows = new List<List<string>>();
		}




	}
}
