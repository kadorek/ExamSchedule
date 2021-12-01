using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExamSchedule.Extensions;
using System.Collections.Generic;

namespace ExamSchedule.Models
{
	public partial class ImportedData
	{
		public ImportedData()
		{

		}

		public long Id { get; set; }
		public string Date { get; set; }
		public string MIMEType { get; set; }
		public byte[] Data { get; set; }


	}


	public partial class ImportedData
	{
		[NotMapped]
		public string Size
		{
			get
			{
				return Data.LongLength.GetSizeInMemory();
			}
		}
	}


	



}