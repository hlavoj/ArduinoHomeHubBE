using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataProvider.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Riganti.Utils.Infrastructure.Core;

namespace ArduinoHomeHubBE.Controllers
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
        private readonly IUnitOfWorkProvider _uowProvider;
        private readonly IQuery<Light> _lightQuery;
        private readonly IRepository<Light, int> _lightRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUnitOfWorkProvider uowProvider, IQuery<Light> lightQuery, IRepository<Light, int> lightRepository)
        {
            _logger = logger;
            _uowProvider = uowProvider;
            _lightQuery = lightQuery;
            _lightRepository = lightRepository;
        }

        public class Sample
        {
            public DateTime timestamp;
            public double value;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var series = new List<Sample>();
            series.Add(new Sample() { value = 2, timestamp = DateTime.Now.AddMinutes(3) });
            series.Add(new Sample() { value = 4, timestamp = DateTime.Now.AddMinutes(4) });
            series.Add(new Sample() { value = 2, timestamp = DateTime.Now.AddMinutes(59) });
            series.Add(new Sample() { value = 1, timestamp = DateTime.Now.AddMinutes(69) });
            series.Add(new Sample() { value = 5, timestamp = DateTime.Now.AddMinutes(79) });
            series.Add(new Sample() { value = 66,timestamp = DateTime.Now.AddMinutes(150) });

            var groups = series.GroupBy(x =>
                {
                    //var a  = x.timestamp.Date.AddHours(x.timestamp.Hour);
                    //return a;
                    var stamp = x.timestamp;
                    stamp = stamp.AddMinutes(-(stamp.Minute % 30));
                    stamp = stamp.Date.AddHours(stamp.Hour).AddMinutes(stamp.Minute);
                    return stamp;
                })
                .Select(g => new { TimeStamp = g.Key, Value = g.Average(s => s.value) })
                .ToList();


            //using (var uow = _uowProvider.Create())
            //{
            //    var res = await _lightQuery.ExecuteAsync();
            //    return Ok(res);
            //}



            var rng = new Random();
            return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray());
        }
    }
}
