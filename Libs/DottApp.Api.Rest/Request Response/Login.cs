using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DottApp.Api.Rest.Request_Response
{
    public class SigninRequest 
    {
        [JsonPropertyName("loginName")]
        public string LoginName { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }

    public class SigninResponse
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
    }
}
