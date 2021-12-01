using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSchedule.Extensions
{
	[AttributeUsage(AttributeTargets.Class)]
	public class HideFromSelect : Attribute
	{
		private string[] propertyNames;
		public HideFromSelect(params string[] _names)
		{
			propertyNames = _names;
		}

		public virtual string[] ProppertyNames { get { return propertyNames; } }

	}
}
