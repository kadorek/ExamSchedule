using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSchedule.Models.ViewModels
{
	public enum MessageType
	{
		Error, Success
	}

	public class SiteException : Exception
	{
		public MessageType Type { get; set; }
		public SiteException(string? msg):base(msg) {
				
		}
		public SiteException(string? msg, Exception inner):base(msg,inner) { }

		public SiteException(string? msg, MessageType mt) : base(msg) {
			this.Type = mt;
		}

		public override string ToString()
		{
			string msg="";
			SiteException e = this;
			msg += e.Message;
			while (e.InnerException != null) {
				e = (SiteException)e.InnerException;
				msg += "\n " + e.Message;
			}
			return msg;
		}


	}
}
