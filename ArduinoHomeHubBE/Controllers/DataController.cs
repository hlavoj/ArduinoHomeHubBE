using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataProvider.Entities;
using Microsoft.AspNetCore.Mvc;
using Riganti.Utils.Infrastructure.Core;


namespace ArduinoHomeHubBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IUnitOfWorkProvider _uowProvider;
        private readonly IRepository<TemperatureData, int> _temperatureRepository;

        public DataController(IUnitOfWorkProvider uowProvider, IRepository<TemperatureData, int> temperatureRepository)
        {
            _uowProvider = uowProvider;
            _temperatureRepository = temperatureRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(double temperature, double humidity, bool sensor1, bool sensor2)
        {
            using (var uow = _uowProvider.Create())
            {
                var data= new TemperatureData
                {
                    DateTime = DateTime.Now,
                    Temperature = temperature,
                    Humidity = humidity,
                    User = null
                };
                _temperatureRepository.Insert(data);
                await uow.CommitAsync();
            }

            return Ok();
        }
    }
}
