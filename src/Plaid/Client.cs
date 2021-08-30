using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Acklann.Plaid
{
	public class Client
	{
		public Client(string baseUrl, string clientId, string secret, IHttpClientFactory factory)
		{
			_secret = secret ?? throw new ArgumentNullException(nameof(secret));
			_baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
			_client_id = clientId ?? throw new ArgumentNullException(nameof(clientId));
			_factory = factory ?? CreateDefaultFactory() ?? throw new ArgumentNullException(nameof(factory));
		}

		/// <summary>
		/// The /link/token/create endpoint creates a link_token, which is required as a parameter when initializing Link. Once Link has been initialized, it returns a public_token, which can then be exchanged for an access_token via /item/public_token/exchange as part of the main Link flow.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <param name="sandbox">if set to <c>true</c> allows you to create a public_token without using Link.</param>
		/// <returns></returns>
		public Task<Response<Link.CreateLinkTokenResponse>> CreateLinkToken(Link.CreateLinkTokenRequest request, bool sandbox = false)
		{
			string path = sandbox ? "/sandbox/public_token/create" : "/link/token/create";
			return SendRequestAsync<Link.CreateLinkTokenRequest, Link.CreateLinkTokenResponse>(path, request);
		}

		public Task<Response<Item.ExchangePublicTokenResponse>> ExchangeTokenForAccessTokenAsync(Item.ExchangePublicTokenRequest request)
		{
			return SendRequestAsync<Item.ExchangePublicTokenRequest, Item.ExchangePublicTokenResponse>("/item/public_token/exchange", request);
		}

		internal async Task<Response<TResponse>> SendRequestAsync<TRequest, TResponse>(string path, TRequest model) where TRequest : RequestBase2
		{
			// Create HTTP Request

			model.Secret = _secret;
			model.ClientId = _client_id;

			string url = GetEndpoint(path);
			string body = System.Text.Json.JsonSerializer.Serialize(model, typeof(TRequest), _serializerOptions);

			var request = new HttpRequestMessage(HttpMethod.Post, url);
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
					TResponse data = System.Text.Json.JsonSerializer.Deserialize<TResponse>(json, _serializerOptions);
					return new Response<TResponse>(data, (int)response.StatusCode);
				}
				else
				{
					var error = System.Text.Json.JsonSerializer.Deserialize<Exceptions.PliadError>(json, _serializerOptions);
					return new Response<TResponse>((int)response.StatusCode, error);
				}
			}
		}

		#region Backing Members

		private readonly IHttpClientFactory _factory;
		private readonly string _client_id, _secret, _baseUrl;
		private readonly System.Text.Json.JsonSerializerOptions _serializerOptions = GetSerializerOptions();

		private string GetEndpoint(string path) => string.Concat(_baseUrl, path);

		private static bool TryDeserialize(string json, out object model)
		{
			throw new System.NotImplementedException();
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
