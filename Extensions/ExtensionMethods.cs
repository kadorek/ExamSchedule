using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSchedule.Extensions
{
	public static class ExtensionMethods
	{

		public static string GetSizeInMemory(this long bytesize)
		{
			string[] sizes = { "B", "KB", "MB", "GB", "TB" };
			double len = Convert.ToDouble(bytesize);
			int order = 0;
			while (len >= 1024D && order < sizes.Length - 1)
			{
				order++;
				len /= 1024;
			}
			return string.Format(CultureInfo.CurrentCulture, "{0:0.##} {1}", len, sizes[order]);
		}

		public static TValue GetAttributeValue<TAttribute, TValue>(
		this Type type,
		Func<TAttribute, TValue> valueSelector)
		where TAttribute : Attribute
		{
			var att = type.GetCustomAttributes(
				typeof(TAttribute), true
			).FirstOrDefault() as TAttribute;
			if (att != null)
			{
				return valueSelector(att);
			}
			return default(TValue);
		}

		public static bool AllIsNull(this object e, params string[] propertyNames)
		{

			bool flag = true;

			foreach (var _p in propertyNames)
			{
				if (e.GetType().GetProperty(_p).GetValue(e) != null)
				{
					flag = false;
					break;
				}
			}

			return flag;

		}





	}
}
