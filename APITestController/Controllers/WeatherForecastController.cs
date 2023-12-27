using Microsoft.AspNetCore.Mvc;

namespace APITestController.Controllers
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

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPost]
        [Route("AddRecordFromForm")]
        public IActionResult AddRecordFromForm([FromForm] WeatherForecastAddModel weather)
        {
            return Ok(weather);
        }
        [HttpPost]
        [Route("AddRecordFromBody")]
        public IActionResult AddRecordFromBody([FromBody] WeatherForecastAddModel weather)
        {
            return Ok(weather);
        }
        [HttpPost]
        [Route("AddRecordUsingQuery")]
        public IActionResult AddRecordUsingQuery(
                    [FromQuery(Name = "Date")] DateTime date,
                    [FromQuery(Name = "TemperatureC")] int temperatureC,
                    [FromQuery(Name = "Summary")] string summary)
        {
            var weather = new WeatherForecastAddModel
            {
                Date = date,
                TemperatureC = temperatureC,
                Summary = summary
            };

            return Ok(weather);
        }

       
    }
}
