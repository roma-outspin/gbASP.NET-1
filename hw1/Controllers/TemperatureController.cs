using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace hw1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureController : ControllerBase
    {
        private static List<WeatherForecast> _statistics = new();


        [HttpPost("create")]
        public IActionResult Create([FromQuery] DateTime dateTime, int temperature)
        {
            _statistics.Add(new WeatherForecast() { TemperatureC = temperature, Date = dateTime });
            return Ok(_statistics.Count);
        }

        [HttpGet]
        public IActionResult Start()
        {
            return Ok($"Количество записей в базе: {_statistics.Count}\n" +
                $"create(dateTime, temperature)\n" +
                $"read\n" +
                $"delete(startTime, endTime)\n" +
                $"update(dateTime, temperature)\n");
        }

        [HttpGet("read")]
        public IActionResult Read()
        {
            return Ok(_statistics);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime startTime, [FromQuery] DateTime endTime)
        {
            try
            {
                int counter = 0;
                int startIndex = -1;
                for (int i = 0; i<_statistics.Count;i++)
                {
                    if (_statistics[i].Date >= startTime && _statistics[i].Date <= endTime)
                    {
                        if (startIndex == -1) startIndex = i;
                        counter++;
                    }
                }
                _statistics.RemoveRange(startIndex, counter);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }


        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime dateTime, int temperature)
        {
            try
            {
                foreach (var item in _statistics)
                {
                    if (item.Date == dateTime)
                    {
                        item.TemperatureC = temperature;
                        break;
                    }
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
