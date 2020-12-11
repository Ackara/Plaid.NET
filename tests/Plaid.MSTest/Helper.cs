﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Acklann.Plaid
{
	public static class Helper
	{
		static Helper()
		{
			string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "secrets.json");
			var document = JObject.Parse(File.ReadAllText(configPath));
			_plaid = document["plaid"];

			Secret = _plaid["secret"].Value<string>();
			ClientId = _plaid["client_id"].Value<string>();
			PublicKey = _plaid["public_key"].Value<string>();
			AccessToken = _plaid?["access_token"]?.Value<string>();
			IntlAccessToken = _plaid?["intl_access_token"]?.Value<string>();
		}

		public static readonly string ClientId, Secret, AccessToken, IntlAccessToken, PublicKey;

		public static TRequest UseDefaults<TRequest>(this TRequest request)
		{
			PropertyInfo[] properties = request.GetType().GetTypeInfo().GetRuntimeProperties().ToArray();
			setProperty(nameof(AuthorizedRequestBase.AccessToken), AccessToken);
			setProperty(nameof(AuthorizedRequestBase.ClientId), ClientId);
			setProperty(nameof(AuthorizedRequestBase.Secret), Secret);
			return request;

			void setProperty(string name, object value)
			{
				PropertyInfo target = properties.FirstOrDefault(p => p.Name == name);
				if (target != null) target.SetValue(request, value);
			}
		}

		public static TRequest UseDefaultsWithNoAccessToken<TRequest>(this TRequest request)
		{
			PropertyInfo[] properties = request.GetType().GetTypeInfo().GetRuntimeProperties().ToArray();
			setProperty(nameof(AuthorizedRequestBase.ClientId), ClientId);
			setProperty(nameof(AuthorizedRequestBase.Secret), Secret);
			return request;

			void setProperty(string name, object value)
			{
				PropertyInfo target = properties.FirstOrDefault(p => p.Name == name);
				if (target != null) target.SetValue(request, value);
			}
		}

		public static TRequest UseIntlDefaults<TRequest>(this TRequest request)
		{
			PropertyInfo[] properties = request.GetType().GetTypeInfo().GetRuntimeProperties().ToArray();
			setProperty(nameof(AuthorizedRequestBase.AccessToken), IntlAccessToken);
			setProperty(nameof(AuthorizedRequestBase.ClientId), ClientId);
			setProperty(nameof(AuthorizedRequestBase.Secret), Secret);
			return request;

			void setProperty(string name, object value)
			{
				PropertyInfo target = properties.FirstOrDefault(p => p.Name == name);
				if (target != null) target.SetValue(request, value);
			}
		}

		public static string ToJson(this object obj)
		{
			return JsonConvert.SerializeObject(obj, Formatting.Indented);
		}

		#region Private Members

		internal const string your_public_key_do_not_have_access_contact_plaid = "Error authenticating public key. Your public key is not enabled for products \"identity\" and \"income\". Please contact Support (https://dashboard.plaid.com/support/new) to be enabled.";
		private static JToken _plaid;

		#endregion Private Members
	}
}
