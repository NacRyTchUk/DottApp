using System;
using System.Collections.Generic;
using System.Text;

namespace DottApp.Api.Rest.Request_Response
{

    public class RegistrationRequest : BaseRequest
    {
        public string LoginName { get; set; }
        public string NickName { get; set; }
        public string PasswordHash { get; set; }
    }

    public class RegistrationResponse 
    {
        
    }
}
