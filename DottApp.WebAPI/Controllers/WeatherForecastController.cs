using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using DottApp.RsaAesWrapper;
using DottApp.Services.Auth;
using DottApp.WebAPI.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace DottApp.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public WeatherForecastController(DatabaseContext context)
        {
            _context = context;
        }


        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //private readonly ILogger<WeatherForecastController> _logger;

        //public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}

        [HttpGet]
        public List<string> Get()
        {
            return db;
        }

        //TODO: Нормальная обработка исключений!
        [HttpPost("Connect")]
        public ConnectionSessionResponse Post([FromBody] ConnectionSessionRequest cl_csrr)
        {
            RSAw rsaw = new RSAw();
            Random rnd = new Random(DateTime.Now.Millisecond);
            string cl_accessToken = string.Empty;
            ConnectionSessionRequest cl_key;
            //try
            //{ cl_key = JsonSerializer.Deserialize<ConnectionSessionRequest>(cl_csr); }
            //catch (JsonException e)
            //{ return JsonSerializer.Serialize<DAException>(new DAException(DAExceptionType.BadRequestParameters)); }


            for (int i = 0; i < 32; i++) cl_accessToken += (char)rnd.Next('a', 'z');
            var csr = new ConnectionSessionResponse
            {
                PublicKey = new RSAByteKey().SetKeyFromParameters(rsaw.PublicKey), 
                AccessToken = new AccessToken().GenNew(), //rsaw.Encrypt("Токен")
                SessionId = rsaw.Encrypt(rnd.Next(0, 100).ToString(), cl_csrr.PublicKey.GetRSAParameters())
            };
            return csr;
        }

        [HttpGet("adduser/Login={login}&NickName={nick}")]
        public IActionResult Get(string login, string nick)
        {
            _context.Users.Add(new User
            {
                LoginName = login,
                NickName = nick
            });

            _context.SaveChanges();

                return Ok();
        }

        private static List<string> db = new List<string>();
        
        [HttpGet("rnd/min={min}/max={max}")]
        public int Get(int min, int max)
        {
            
            var rnd = new Random();
            return rnd.Next(Math.Min((int)min, (int)max), Math.Max((int)min, (int)max));
        }

        [HttpPost]
        public void Post([FromBody] string body, string s)
        {
            db.Add(body);
        }
    }
}