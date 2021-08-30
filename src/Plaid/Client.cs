using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Acklann.Plaid
{
	public class Client
	{
		public Client(Environment environment, string clientId, string secret)
			: this(GetBaseUrl(environment), clientId, secret, VERSION, default)
		{
		}

		public Client(string baseUrl, string clientId, string secret, string version = VERSION, IHttpClientFactory factory = default)
		{
			_secret = secret ?? throw new ArgumentNullException(nameof(secret));
			_baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
			_client_id = clientId ?? throw new ArgumentNullException(nameof(clientId));
			_version = version ?? throw new ArgumentNullException(nameof(version));
			_factory = factory ?? CreateDefaultFactory() ?? throw new ArgumentNullException(nameof(factory));
		}

		/// <summary>
		/// The /link/token/create endpoint creates a link_token, which is required as a parameter when initializing Link. Once Link has been initialized, it returns a public_token, which can then be exchanged for an access_token via /item/public_token/exchange as part of the main Link flow.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <param name="sandbox">if set to <c>true</c> allows you to create a public_token without using Link.</param>
		/// <returns></returns>
		public Task<Response<Token.CreateLinkTokenResponse>> CreateLinkTokenAsync(Token.CreateLinkTokenRequest request, bool sandbox = false)
		{
			string path = sandbox ? "/sandbox/public_token/create" : "/link/token/create";
			return PostRequest<Token.CreateLinkTokenRequest, Token.CreateLinkTokenResponse>(path, request);
		}

		/// <summary>
		/// Use the /sandbox/public_token/create endpoint to create a valid public_token for an arbitrary institution ID, initial products, and test credentials.
		/// The created public_token maps to a new Sandbox Item. You can then call <see cref="ExchangeTokenForAccessTokenAsync(Item.ExchangePublicTokenRequest)"/> to exchange the public_token for an access_token and perform all API actions.
		/// </summary>
		/// <param name="request">The request.</param>
		public Task<Response<Sandbox.CreatePublicTokenResponse>> CreateSandboxPublicTokenAsync(Sandbox.CreatePublicTokenRequest request)
		{
			return PostRequest<Sandbox.CreatePublicTokenRequest, Sandbox.CreatePublicTokenResponse>("/sandbox/public_token/create", request);
		}

		/// <summary>
		/// The /item/public_token/exchange endpoint to exchange a Link public_token for an API access_token. Link hands off the public_token client-side via the onSuccess callback once a user has successfully created an Item. The public_token is ephemeral and expires after 30 minutes.
		/// </summary>
		/// <param name="request">The request.</param>
		public Task<Response<Token.ExchangePublicTokenResponse>> ExchangeTokenForAccessTokenAsync(Token.ExchangePublicTokenRequest request)
		{
			return PostRequest<Token.ExchangePublicTokenRequest, Token.ExchangePublicTokenResponse>("/item/public_token/exchange", request);
		}

		/// <summary>
		/// The /institutions/get endpoint; returns all financial institutions currently supported by Plaid.
		/// </summary>
		/// <param name="request">The request.</param>
		public Task<Response<Institution.GetAllInstitutionsResponse>> GetAllInstituionsAsync(Institution.GetAllInstitutionsRequest request)
		{
			return PostRequest<Institution.GetAllInstitutionsRequest, Institution.GetAllInstitutionsResponse>("/institutions/get", request);
		}

		/// <summary>
		/// The /institutions/get_by_id endpoint; return details on a specified financial institution currently supported by Plaid.
		/// </summary>
		/// <param name="request">The request.</param>
		public Task<Response<Institution.GetInstitutionByIdResponse>> GetInstitutionByIdAsync(Institution.GetInstitutionByIdRequest request)
		{
			return PostRequest<Institution.GetInstitutionByIdRequest, Institution.GetInstitutionByIdResponse>("/institutions/get_by_id", request);
		}

		/// <summary>
		/// The /item/get endpoint; returns information about the status of an <see cref="Entity.Item"/>.
		/// </summary>
		/// <param name="request">The request.</param>
		public Task<Response<Item.GetItemResponse>> GetItemAsync(Item.GetItemRequest request)
		{
			return PostRequest<Item.GetItemRequest, Item.GetItemResponse>("/item/get", request);
		}

		/// <summary>
		/// The /transactions/get endpoint; retrieves and refresh 24 months of historical transaction data, including geolocation, merchant, and category information.
		/// </summary>
		/// <param name="request">The request.</param>
		public Task<Response<Transactions.GetTransactionsResponse>> GetTransactionsAsync(Transactions.GetTransactionsRequest request)
		{
			return PostRequest<Transactions.GetTransactionsRequest, Transactions.GetTransactionsResponse>("/transactions/get", request);
		}

		/// <summary>
		/// The /transactions/refresh endpoint; It initiates an on-demand extraction to fetch the newest transactions for an Item. This on-demand extraction takes place in addition to the periodic extractions that automatically occur multiple times a day for any Transactions-enabled Item. If changes to transactions are discovered after calling /transactions/refresh, Plaid will fire a webhook: TRANSACTIONS_REMOVED will be fired if any removed transactions are detected, and DEFAULT_UPDATE will be fired if any new transactions are detected. New transactions can be fetched by calling /transactions/get.
		/// </summary>
		/// <param name="request">The request.</param>
		public Task<Response<Transactions.RefreshTransactionResponse>> RefreshTransactionData(Transactions.RefreshTransactionRequest request)
		{
			return PostRequest<Transactions.RefreshTransactionRequest, Transactions.RefreshTransactionResponse>("/transactions/refresh", request);
		}

		/// <summary>
		/// The /transactions/refresh endpoint; It initiates an on-demand extraction to fetch the newest transactions for an Item. This on-demand extraction takes place in addition to the periodic extractions that automatically occur multiple times a day for any Transactions-enabled Item. If changes to transactions are discovered after calling /transactions/refresh, Plaid will fire a webhook: TRANSACTIONS_REMOVED will be fired if any removed transactions are detected, and DEFAULT_UPDATE will be fired if any new transactions are detected. New transactions can be fetched by calling /transactions/get.
		/// </summary>
		/// <param name="accessToken">The access token.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">accessToken</exception>
		public Task<Response<Transactions.RefreshTransactionResponse>> RefreshTransactionData(string accessToken)
		{
			if (string.IsNullOrEmpty(accessToken)) throw new ArgumentNullException(nameof(accessToken));
			return RefreshTransactionData(new Transactions.RefreshTransactionRequest(accessToken));
		}

		/// <summary>
		/// The /categories/get endpoint; get detailed information on categories returned by Plaid.
		/// </summary>
		public Task<Response<Transactions.GetCategoriesResponse>> GetCategoriesAsync()
		{
			return PostRequest<Transactions.GetCategoriesRequest, Transactions.GetCategoriesResponse>("/categories/get", new Transactions.GetCategoriesRequest());
		}

		/// <summary>
		/// The /accounts/balance/get endpoint;  returns the real-time balance for each of an Item's accounts.
		/// While other endpoints may return a balance object, only /accounts/balance/get forces the available and current balance fields to be refreshed rather than cached.
		/// This endpoint can be used for existing Items that were added via any of Plaid’s other products. This endpoint can be used as long as Link has been initialized with any other product, balance itself is not a product that can be used to initialize Link.
		/// </summary>
		/// <param name="request">The request.</param>
		public Task<Response<Balance.GetBalanceResponse>> GetAccountsBalanceAsync(Balance.GetBalanceRequest request)
		{
			return PostRequest<Balance.GetBalanceRequest, Balance.GetBalanceResponse>("/accounts/balance/get", request);
		}

		internal async Task<Response<TResponse>> PostRequest<TRequest, TResponse>(string path, TRequest model) where TRequest : RequestBase2 where TResponse : PlaidResponseBase
		{
			// Create HTTP Request

			string url = GetEndpoint(path);
			string body = System.Text.Json.JsonSerializer.Serialize(model, typeof(TRequest), _serializerOptions);

			var request = new HttpRequestMessage(HttpMethod.Post, url);
			request.Headers.Add("Plaid-Version", _version);
			request.Headers.Add("PLAID-SECRET", _secret);
			request.Headers.Add("PLAID-CLIENT-ID", _client_id);
			request.Content = new StringContent(body, System.Text.Encoding.UTF8, "application/json"); ;
#if DEBUG
			WriteToDebugger(request, body);
#endif
			HttpClient client = _factory.CreateClient(nameof(Client));
			using (var response = await client.SendAsync(request))
			{
				// Build Response

				string json = await response.Content.ReadAsStringAsync();
#if DEBUG
				WriteToDebugger(response, json);
#endif
				if (response.IsSuccessStatusCode)
				{
					bool parsed = TryDeserialize(json, out TResponse data);
					if (parsed) return new Response<TResponse>(data, (int)response.StatusCode);
					else return new Response<TResponse>(400, CreateInvalidResultError(typeof(TResponse)));
				}
				else
				{
					var error = System.Text.Json.JsonSerializer.Deserialize<Exceptions.PliadError>(json, _serializerOptions);
					return new Response<TResponse>((int)response.StatusCode, error);
				}
			}
		}

		#region Backing Members

		private const string VERSION = "2020-09-14";
		private readonly IHttpClientFactory _factory;
		private readonly string _client_id, _secret, _baseUrl, _version;
		private readonly System.Text.Json.JsonSerializerOptions _serializerOptions = GetSerializerOptions();

		private string GetEndpoint(string path) => string.Concat(_baseUrl, path);

		private bool TryDeserialize<T>(string json, out T model)
		{
			try
			{
				model = System.Text.Json.JsonSerializer.Deserialize<T>(json, _serializerOptions);
				return true;
			}
			catch (System.Text.Json.JsonException ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }
			catch (ArgumentException) { }

			model = default;
			return false;
		}

		private static string GetBaseUrl(Environment environment)
		{
			return environment switch
			{
				Environment.Production => "https://production.plaid.com",
				Environment.Development => "https://development.plaid.com",
				_ => "https://sandbox.plaid.com",
			};
		}

		private static IHttpClientFactory CreateDefaultFactory()
		{
			return new ServiceCollection()
				.AddHttpClient()
				.BuildServiceProvider()
				.GetService<IHttpClientFactory>();
		}

		private static System.Text.Json.JsonSerializerOptions GetSerializerOptions()
		{
			var options = new System.Text.Json.JsonSerializerOptions
			{
#if DEBUG
				WriteIndented = true,
#endif
				DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
			};
			return options;
		}

		private static Exceptions.PliadError CreateInvalidResultError(Type type)
		{
			return new Exceptions.PliadError
			{
				Type = "INVALID_RESULT",
				Message = $"An unexpected error occurred while de-serializing '{type.FullName}'",
				Code = null,
			};
		}

		private static void WriteToDebugger(HttpRequestMessage request, string body)
		{
			System.Diagnostics.Debug.Write("REQUEST:::");
			System.Diagnostics.Debug.WriteLine($"{request.Method} {request.RequestUri}");
			System.Diagnostics.Debug.WriteLine(body);
		}

		private static void WriteToDebugger(HttpResponseMessage request, string body)
		{
			System.Diagnostics.Debug.Write("RESPONSE:::");
			System.Diagnostics.Debug.WriteLine($"{(int)request.StatusCode} ({request.StatusCode}) {request.RequestMessage.RequestUri}");
			System.Diagnostics.Debug.WriteLine(body);
		}

		#endregion Backing Members
	}
}
