namespace DemoAPI
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int? TemperatureC { get; set; }

        public int TemperatureF
        {
            get
            {
                if (TemperatureC != null)
                    return 32 + (int)(TemperatureC / 0.5556);
                else
                    return 0;
            }
        }

        public string? Summary { get; set; }

        public double? Latitude { get; set; }
        
        public double? Longitude { get; set; }
    }
}
