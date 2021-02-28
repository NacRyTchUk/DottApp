using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DottApp.RsaAesWrapper;
using DottApp.Services.Auth;

namespace DottApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {


        [HttpPost("NewConnectSession")]
        public ConnectionSessionResponse Post([FromBody]ConnectionSessionRequest csRequest)
        {
            RSAw host_rsaw = new RSAw();
            Random rnd = new Random(DateTime.Now.Millisecond);
            
            string cl_accessToken = string.Empty;
            for (int i = 0; i < 32; i++) cl_accessToken += (char)rnd.Next('a', 'z');

            var csResponse = new ConnectionSessionResponse
            {
                PublicKey = new RSAByteKey().SetKeyFromParameters(host_rsaw.PublicKey),
                AccessToken = host_rsaw.Encrypt("Токен"), // new AccessToken().GenNew()
                SessionId = host_rsaw.Encrypt(rnd.Next(0, 100).ToString(), csRequest.PublicKey.GetRSAParameters())
            };
            return csResponse;
        }




    }
}
