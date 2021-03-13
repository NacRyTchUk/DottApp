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

        [HttpGet("ConnectList")]
        public List<string> ConnectList()
        {
            List<string> res = new List<string>();
            foreach (var connect in _context.ActiveConnections)
                res.Add(connect.LoginName);
            return res;
        }

        [HttpGet("AuthUsersList")]
        public List<string> AuthUsersList()
        {
            List<string> res = new List<string>();
            foreach (var connect in _context.ActiveConnections.Where(a=>a.IsAuth == true))
                res.Add(connect.LoginName);
            return res;
        }



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
                AesKey = Convert.ToBase64String(aesw.Key)
            });
            _context.SaveChanges();

            Console.WriteLine("done");
            return csResponse;
        }

        [HttpGet("SignUp")]
        public string SignUp(string sid, string body)
        {
            Console.WriteLine("new re req");
            Console.WriteLine(sid);
            Response.ContentType = "text/plain";
            var session = _context.ActiveConnections.Where(cont => cont.SessionId == sid).OrderBy(a => a.Id).Last();
            AESw aesw = new AESw(Convert.FromBase64String(session.AesKey));
            
            var regRequest = aesw.Deserialize<RegistrationRequest>(body);
            
            if (_context.Users.Where(user => user.LoginName == regRequest.LoginName).ToArray().Length == 0 &&
                session.Id != 0)
            {
                var hashSalt = BCrypt.Net.BCrypt.GenerateSalt();
                var newUser = _context.Users.Add(new User()
                {
                    LoginName = regRequest.LoginName,
                    NickName = regRequest.NickName,
                    Salt = hashSalt,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(regRequest.Password + hashSalt),
                    RegistrationDate = DateTime.Now,
                    LastOnline = DateTime.Now
                });
                
                session.IsAuth = true;
                session.LoginName = newUser.Entity.LoginName;
                _context.SaveChanges();
                Console.WriteLine("reg done");
                return aesw.Serialize(new RegistrationResponse()
                {
                    AccessToken = session.AccessToken
                });
            } 
            Console.WriteLine("reg fail");
            return string.Empty;
        }

        [HttpGet("SignIn")]
        public string SignIn(string sid, string body)
        {
            Console.WriteLine("new re log");
            Response.ContentType = "text/plain";
            var session = _context.ActiveConnections.Where(cont => cont.SessionId == sid).ToArray()[0];
            AESw aesw = new AESw(Convert.FromBase64String(session.AesKey));

            var loginRequest = aesw.Deserialize<SigninRequest>(body);

            var loginName = loginRequest.LoginName;

            var user = _context.Users.Where(a => a.LoginName == loginName).ToArray()[0];

            if (user is null) return string.Empty;

            if (BCrypt.Net.BCrypt.Verify(loginRequest.Password + user.Salt, user.PasswordHash))
            {
                session.IsAuth = true;
                session.LoginName = loginName;
                _context.SaveChanges();
                Console.WriteLine("log done " + session.AccessToken);
                return aesw.Serialize(new SigninResponse()
                {
                    AccessToken = session.AccessToken
                });
            }
            Console.WriteLine("log fail");

            return string.Empty;
        }

        
    }
}
