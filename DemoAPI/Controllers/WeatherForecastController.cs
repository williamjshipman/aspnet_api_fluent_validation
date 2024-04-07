using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IValidator<WeatherForecast> _validator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IValidator<WeatherForecast> validator)
        {
            _logger = logger;
            _validator = validator;
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

        [HttpPut(Name = "AddWeatherForecast")]
        public IActionResult Put([FromBody] WeatherForecast weatherForecast)
        {
            var result = _validator.Validate(weatherForecast);
            if (!result.IsValid)
            {
                return BadRequest(result.ToString());
            }
            return Ok();
        }
    }
}
