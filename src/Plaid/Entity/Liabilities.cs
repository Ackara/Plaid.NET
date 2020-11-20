using Newtonsoft.Json;
using System;

namespace Acklann.Plaid.Entity
{
	public class Liabilities
	{
		[JsonProperty("credit")]
		public Credit[] Credit { get; set; }

		[JsonProperty("mortgage")]
		public Mortgage[] Mortgage { get; set; }

		[JsonProperty("student")]
		public Student[] Student { get; set; }
	}
}
