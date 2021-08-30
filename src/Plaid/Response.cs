namespace Acklann.Plaid
{
	public readonly struct Response
	{
		public Response(int statusCode, Exceptions.PliadError error = default)
		{
			StatusCode = statusCode;
			Error = error;
		}

		public int StatusCode { get; }

		public Exceptions.PliadError Error { get; }

		public string Message { get => Error.Message; }

		public bool Succeeded { get => IsGood(StatusCode); }

		public override string ToString() => Format(StatusCode, Message);

		internal static bool IsGood(int code) => code >= 200 && code <= 299;

		internal static string Format(int code, string message) => $"({code}): {message}".Trim(' ', ':');

		#region operators

		public static implicit operator bool(Response x) => IsGood(x.StatusCode);

		#endregion operators
	}

	public readonly struct Response<T>
	{
		public Response(int statusCode, Exceptions.PliadError error = default)
		{
			Data = default;
			Error = error;
			StatusCode = statusCode;
		}

		public Response(T data, int statusCode = 200, Exceptions.PliadError error = default)
		{
			Data = data;
			Error = error;
			StatusCode = statusCode;
		}

		public int StatusCode { get; }

		public Exceptions.PliadError Error { get; }

		public string Message { get => Error.Message; }

		public T Data { get; }

		public bool Succeeded { get => Response.IsGood(StatusCode); }

		public override string ToString() => Response.Format(StatusCode, Message);

		#region operators

		public static implicit operator T(Response<T> x) => x.Data;

		public static explicit operator bool(Response<T> x) => Response.IsGood(x.StatusCode);

		public static implicit operator Response(Response<T> x) => new Response(x.StatusCode, x.Error);

		#endregion operators
	}
}
