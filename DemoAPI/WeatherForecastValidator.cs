using DemoAPI.Controllers;
using FluentValidation;

namespace DemoAPI
{
    public class WeatherForecastValidator : AbstractValidator<WeatherForecast>
    {
        public WeatherForecastValidator()
        {
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.TemperatureC).NotNull().InclusiveBetween(-20, 55);
            RuleFor(x => x.Summary).NotEmpty();
            RuleFor(x => x.Latitude).NotNull().InclusiveBetween(-90, 90);
            RuleFor(x => x.Longitude).NotNull().InclusiveBetween(-180, 180);
            RuleFor(x => x.Summary).Must(
                x => WeatherForecastController.Summaries
                    .Contains(x, StringComparer.InvariantCultureIgnoreCase))
                .WithMessage("Summary is not one of " + string.Join(", ", WeatherForecastController.Summaries) + ".");
        }
    }
}
