using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ExamSchedule.Models.ViewModels
{
	public enum MessageType
	{
		Error, Success
	}

	public class SiteMessage
	{
		public MessageType Type { get; set; }
		public string Message { get; private set; }
		public SiteMessage InnerMessage { get; private set; }
		public object Data{ get;  set; }
		public string TypeAsText  { get { return Type.ToString(); } }
		public SiteMessage(string msg) {
			this.Message = msg;
		}
		public SiteMessage(string msg, MessageType mt, SiteMessage inner) {
			this.Message = msg;
			this.InnerMessage = inner;
			this.Type = mt;

		}

		public SiteMessage(string msg, MessageType mt)  {
			this.Message = msg;
			this.Type = mt;
		}

		public override string ToString()
		{
			string msg="";
			SiteMessage e = this;
			msg += e.Message;
			while (e.InnerMessage != null) {
				e = (SiteMessage)e.InnerMessage;
				msg += "\n " + e.Message;
			}
			return msg;
		}


	}
}
