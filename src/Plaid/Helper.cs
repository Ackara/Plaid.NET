using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Acklann.Plaid
{
	internal static class Helper
	{
		public static string ToPlaidDate(this DateTime date) => date.ToString("yyyy-MM-dd");
	}
}
