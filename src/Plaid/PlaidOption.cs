namespace Acklann.Plaid
{
	public class PlaidOption
	{
		public PlaidOption()
		{
			Version = "2020-09-14";
			EnvironmentName = Environment.Production;
		}

		public Environment EnvironmentName { get; set; }

		public string Version { get; set; }

		public string ClientId { get; set; }

		public string Secrets { get; set; }

		public string AccessToken { get; set; }
	}
}
