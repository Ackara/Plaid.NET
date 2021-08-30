using System.Text.Json.Serialization;

namespace Acklann.Plaid.Exceptions
{
	public struct PliadError
	{
		[JsonPropertyName("display_message")]
		public string DisplayMessage { get; set; }

		[JsonPropertyName("documentation_url")]
		public string DocumentUrl { get; set; }

		[JsonPropertyName("error_code")]
		public string Code { get; set; }

		[JsonPropertyName("error_message")]
		public string Message { get; set; }

		[JsonPropertyName("error_type")]
		public string Type { get; set; }

		[JsonPropertyName("request_id")]
		public string RequestId { get; set; }

		[JsonPropertyName("suggested_action")]
		public string SuggestedAction { get; set; }
	}
}
