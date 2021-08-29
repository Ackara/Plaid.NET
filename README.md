# Plaid.NET
[![NuGet](https://img.shields.io/nuget/v/Acklann.Plaid.svg?style=plastic)](https://www.nuget.org/packages/Acklann.Plaid/)
---

## What is Plaid.NET?
Plaid.NET is a .netstandard library for interacting with [Plaid's](https://plaid.com/) banking APIs. See their documentation [here](https://plaid.com/docs/api/).

### Where can I get it?
Plaid.NET is available at [nuget.org](https://www.nuget.org/packages/Acklann.Plaid).

Package Manager `PM > Install-Package Acklann.Plaid.NET`

### How it works?
You can make all calls to Plaid's API via the `Acklann.Plaid.PlaidClient` class.

```c#
var client = new PlaidClient();
// Retrieving a user's recent transactions.
var result = await client.FetchTransactionsAsync(new GetTransactionsRequest()
{
    Secret = "your_secret",
    ClientId = "your_client_id",
    AccessToken = "your_token",
    ...
});
```

### Testing

For contributors who want to run the unit tests; there is `secrets.json` file that is intentionally excluded from the project. The file stores your Plaid sandbox keys. The `Helper.cs` utilize this file. Also there is a project called Plaid.Demo; run the project to generate a access token.

```json
"plaid":
  {
    "secret": null,
    "client_id": null,
    "public_key": null,
    "access_token": null
  }
```

## API Version
Plaid.NET currently targets Plaid API version `2019-05-29`.
