﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DottApp.Api.Rest
{
    public class BaseRequest
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
    }
}
