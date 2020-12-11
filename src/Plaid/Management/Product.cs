namespace Acklann.Plaid.Management
{
	/// <summary>
	/// Supported products names
	/// </summary>
	public class Product
	{
		public static readonly string
			Transaction = "transactions",
			Auth = "auth",
			Identity = "identity",
			Assests = "assets",
			Investments = "investments",
			Liabilities = "payment_initiation",
			PaymentInitiation = "payment_initiation";
	}
}
