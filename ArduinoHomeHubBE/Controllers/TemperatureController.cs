using System;
using System.Linq;
using System.Threading.Tasks;
using DataProvider.DTOs;
using DataProvider.Queries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Riganti.Utils.Infrastructure.Core;

namespace ArduinoHomeHubBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TemperatureController : ControllerBase
    {
        private readonly ITemperatureQuery _temperatureQuery;
        private readonly IUnitOfWorkProvider _uowProvider;

        public TemperatureController(ITemperatureQuery temperatureQuery, IUnitOfWorkProvider uowProvider)
        {
            _temperatureQuery = temperatureQuery;
            _uowProvider = uowProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DateTime? from, [FromQuery] DateTime? to, SamplingInterval? interval)
        {
            try
            {
                _temperatureQuery.UserId = 1;
                _temperatureQuery.From = from;
                _temperatureQuery.To = to;
                _temperatureQuery.Interval = interval;

                using (var uow = _uowProvider.Create())
                {
                    return Ok((await _temperatureQuery.ExecuteAsync()).OrderBy(o => o.DateTime));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}