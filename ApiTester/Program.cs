using System.Net.Http.Headers;

namespace ApiTester
{
    internal class Program
    {
        static private async void HandleResponse(HttpResponseMessage? response, string task)
        {
            if (response == null)
            {
                Console.WriteLine($"{task}: Response is null");
                return;
            }

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{task}: Success: " + content);
            }
            else
            {
                Console.WriteLine($"{task}: Error: " + response.StatusCode);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{task}: Error body: " + content);
            }
        }

        private static async Task Test()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7123");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.UserAgent.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/problem+json"));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("ApiTester", "1.0"));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(".NET", Environment.Version.ToString()));

            HandleResponse(await client.GetAsync("WeatherForecast"), "GET WeatherForecast");
            HandleResponse(await client.PutAsync("WeatherForecast", new StringContent("{\"Date\":\"2022-01-01\",\"TemperatureC\":10,\"Summary\":\"Cool\",\"Latitude\":0,\"Longitude\":0}", new MediaTypeHeaderValue("application/json"))), "PUT WeatherForecast");
            HandleResponse(await client.PutAsync("WeatherForecast", new StringContent("{\"Date\":\"2022-01-01\",\"TemperatureC\":10,\"Summary\":\"invalid\",\"Latitude\":0,\"Longitude\":0}", new MediaTypeHeaderValue("application/json"))), "PUT WeatherForecast");
            HandleResponse(await client.PutAsync("WeatherForecast", new StringContent("{\"Date\":\"2022-01-01\",\"TemperatureC\":10,\"Summary\":\"Cool\",\"Latitude\":-90,\"Longitude\":-1800}", new MediaTypeHeaderValue("application/json"))), "PUT WeatherForecast");

            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            Test().Wait();
        }
    }
}
