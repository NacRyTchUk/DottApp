using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DottApp.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<string> Get()
        {
            return db;
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