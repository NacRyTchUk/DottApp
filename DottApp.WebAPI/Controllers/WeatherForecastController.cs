using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using DottApp.RsaAesWrapper;
using DottApp.WebAPI.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        [HttpGet("Encrypt={data}")]
        public KeyValuePair<byte[], byte[]> Get(string data)
        {
            RSAw rsaw = new RSAw(1024 * 2);
            var pbKey = rsaw.PublicKey;
            var prKey = rsaw.PrivateKey;
            ArrayBufferWriter<byte> abw = new ArrayBufferWriter<byte>();
            KeyValuePair<byte[], byte[]> key =
                new KeyValuePair<byte[], byte[]>(pbKey.Exponent, pbKey.Modulus);
            return key;
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
        public void Post([FromBody] string body)
        {
            db.Add(body);
        }
    }
}