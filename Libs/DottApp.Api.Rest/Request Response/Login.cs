using System;
using System.Collections.Generic;
using System.Text;

namespace DottApp.Api.Rest.Request_Response
{
    public class SigninRequest : BaseRequest
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
    }

    public class SigninResponse
    {
        public string AccessToken { get; set; }
    }
}
