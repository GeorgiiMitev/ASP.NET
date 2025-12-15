using Microsoft.AspNetCore.Mvc;

namespace SocialMediaPlatform.Controllers
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
        public WeatherForecast AddInfo(WeatherForecast forecast)
        {
            return forecast;
        }

        [HttpPut("{id}")]
        public int UpdateWeather([FromRoute] int id)
        {
            return id;
        }

        [HttpDelete("{id}")]
        public int DeleteWeather([FromQuery] int id)
        {
            return id;

        }
        [HttpOptions]
        public int GetAllOptions([FromQuery] int id)
        {
            return id;

        }

    }
}
