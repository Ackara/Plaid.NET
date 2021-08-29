﻿namespace Acklann.Plaid.Item
{
    /// <summary>
    /// Represents a request for plaid's '/item/get' endpoint. The POST /item/get endpoint returns information about the status of an <see cref="Entity.Item"/>.
    /// </summary>
    /// <seealso cref="Acklann.Plaid.AuthorizedRequestBase" />
    public class GetItemRequest : AuthorizedRequestBase { }
}
