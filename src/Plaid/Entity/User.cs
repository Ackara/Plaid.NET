﻿using Newtonsoft.Json;

namespace Acklann.Plaid.Entity
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the client user id.
        /// </summary>
        /// <value>The client user id.</value>
        [JsonProperty("client_user_id")]
        public string ClientUserId { get; set; }

        /// <summary>
        /// Gets or sets the client phone number.
        /// </summary>
        /// <value>The client phone number.</value>
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the verification time of the client phone number.
        /// </summary>
        /// <value>The verification time of the client phone number.</value>
        [JsonProperty("phone_number_verified_time")]
        public string PhoneNumberVerifiedTime { get; set; }
    }
}
