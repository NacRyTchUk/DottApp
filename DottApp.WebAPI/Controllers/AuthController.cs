using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            RSACryptoServiceProvider host_rsaw = new RSACryptoServiceProvider();
            RSACryptoServiceProvider client_rsaw = RSAKeys.ImportPublicKey(csRequest.PublicKey);
            Random rnd = new Random(DateTime.Now.Millisecond);
            
            string cl_accessToken = string.Empty;
            for (int i = 0; i < 32; i++) cl_accessToken += (char)rnd.Next('a', 'z');

            var csResponse = new ConnectionSessionResponse
            {
                PublicKey = RSAKeys.ExportPublicKey(host_rsaw),
                AccessToken = Convert.ToBase64String(client_rsaw.Encrypt(Encoding.UTF8.GetBytes("Токен"), false)), // new AccessToken().GenNew()
                SessionId = Convert.ToBase64String(client_rsaw.Encrypt(Encoding.UTF8.GetBytes(rnd.Next(0, 100).ToString()), false))
            };
            return csResponse;
        }




    }
}
