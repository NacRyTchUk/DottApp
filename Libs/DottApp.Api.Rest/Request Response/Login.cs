using System;
using System.Collections.Generic;
using System.Text;

namespace DottApp.Api.Rest.Request_Response
{
    class LoginRequest : BaseRequest
    {
        public string LoginName { get; set; }
        public string PasswordHash { get; set; }
    }
}
