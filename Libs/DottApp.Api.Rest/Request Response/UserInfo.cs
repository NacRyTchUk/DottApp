using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DottApp.Api.Rest.Request_Response
{
    public class UserInfo
    {
        [JsonPropertyName("accessToken")]
        public DateTime RegistrationDate { get; set; }
    }
}
