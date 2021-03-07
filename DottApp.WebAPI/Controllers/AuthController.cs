using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DottApp.Api.Rest;
using DottApp.Api.Rest.Request_Response;
using DottApp.RsaAesWrapper;
using DottApp.Services.Auth;
using DottApp.WebAPI.Models;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Security;

namespace DottApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public AuthController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("IsLoginFree")]
        public bool IsLoginFree(string login) =>
            _context.Users.Where(a => a.LoginName == login).ToArray().Length == 0;



        [HttpPost("Connect")]
        public ConnectionSessionResponse Connect([FromBody]ConnectionSessionRequest csRequest)
        {
            Console.WriteLine("new conenct");

            
            var rnd = new SecureRandom();
            
            string cl_accessToken = string.Empty;
            for (int i = 0; i < 32; i++) cl_accessToken += (char)rnd.Next('a', 'z');


            AESw aesw = new AESw();
            var sid = string.Empty;
            for (int i = 0; i < 32; i++) sid += (char)rnd.Next('0', '9');


            var csResponse = new ConnectionSessionResponse
            {
                Key = Convert.ToBase64String(RSAKeys.ImportPublicKey(csRequest.Key).Encrypt(aesw.Key, false)),
                SessionId = sid
            };

            _context.ActiveConnections.Add(new ActiveConnection()
            {
                AccessToken = new AccessToken().GenNew(),
                SessionId = sid,
                ConnectionDate = DateTime.Now,
                IsAuth = false,
                AesKey = Convert.ToBase64String(new AESw().Key)
            });
            _context.SaveChanges();

            Console.WriteLine("done");
            return csResponse;
        }

        [HttpPost("SignUp")]
        public string SignUp([FromBody] string body, string sid)
        {
            Console.WriteLine("new re req");
            Console.WriteLine(sid);
            var session =  _context.ActiveConnections.Last(cont => cont.SessionId == sid);
            AESw aesw = new AESw(Convert.FromBase64String(session.AesKey));
            var regRequest = AesJson.Deserialize<RegistrationRequest>(body, aesw);

            if (_context.Users.Where(user => user.LoginName == regRequest.LoginName).ToArray().Length == 0 &&
                session.Id != 0)
            {
                _context.Users.Add(new User()
                {
                    LoginName = regRequest.LoginName,
                    NickName = regRequest.NickName,
                    PasswordHash = regRequest.Password
                });
                
                var activeSession = _context.ActiveConnections.Find(session.Id);
                activeSession.IsAuth = true;
                activeSession.LoginName = regRequest.LoginName;
                _context.SaveChanges();
                Console.WriteLine("reg done");
                return AesJson.Serialize(new RegistrationResponse()
                {
                    AccessToken = new AESw(Convert.FromBase64String(activeSession.AesKey)).Encrypt(activeSession.AccessToken)
                }, aesw);
            } 
            Console.WriteLine("reg fail");

            return string.Empty;
        }

        [HttpPost("SignIn")]
        public string SignIn([FromBody] string body, string sid)
        {
            Console.WriteLine("new re log");
            Console.WriteLine(sid);
            var session = _context.ActiveConnections.Last(cont => cont.SessionId == sid);
            AESw aesw = new AESw(Convert.FromBase64String(session.AesKey));
            var lognRequest = AesJson.Deserialize<SigninRequest>(body, aesw);

            var sessionid = _context.ActiveConnections.Where(cont => cont.SessionId == sid).ToArray()[0].Id;
            var user = _context.Users.Last(a => a.LoginName == lognRequest.LoginName);
            if (user is null) return String.Empty;
            if (BCrypt.Net.BCrypt.Verify(lognRequest.Password, user.PasswordHash))
            {
                var activeSession = _context.ActiveConnections.Find(sessionid);
                activeSession.IsAuth = true;
                activeSession.LoginName = lognRequest.LoginName;
                _context.SaveChanges();
                Console.WriteLine("log done");
                return AesJson.Serialize(new SigninResponse()
                {
                    AccessToken = new AESw(Convert.FromBase64String(activeSession.AesKey)).Encrypt(activeSession.AccessToken)
                }, aesw);
            }
            Console.WriteLine("log fail");

            return String.Empty;
        }
    }
}
