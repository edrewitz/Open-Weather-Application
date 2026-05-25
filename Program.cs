using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;


namespace OpenWeather
{
    public class unitConversion
    {
        public static double toMph(double windSpeedKmh)
        {
            return (windSpeedKmh * 0.621371);
        }

        public static double toFahrenheit(double temperatureCelsius)
        {
            return (temperatureCelsius * 9 / 5) + 32;
        }
    }

    class WeatherResponse
    {
        public string time { get; set; }
        public double temperature { get; set; }
        public double windspeed { get; set; }
        public int winddirection { get; set; }
    }
    class Program
    {
        static async Task getData(string latitude,
                                  string longitude)
        {

            string url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true";

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var currentWeatherElement = JsonDocument.Parse(jsonString).RootElement.GetProperty("current_weather");
            var data = JsonSerializer.Deserialize<WeatherResponse>(currentWeatherElement);
            var time = data.time;
            var dateTime = DateTime.Parse(time);
            var localTime = dateTime.ToLocalTime();
            Console.WriteLine($"\nTime: {localTime}\n");
            Console.WriteLine($"Temperature: {Math.Round(unitConversion.toFahrenheit(data.temperature))}°F");
            Console.WriteLine($"Wind Speed: {Math.Round(unitConversion.toMph(data.windspeed))} mph");
            Console.WriteLine($"Wind Direction: {data.winddirection}°");
        }

        public static async Task Main(string[] args)
        {
            Console.WriteLine("This application retrieves the current weather from the Open-Meteo API for a specified location.\n");

            while (true)
            {
                Console.WriteLine($"Enter a latitude");
                var latitude = Console.ReadLine();
                Console.WriteLine($"Enter a longitude");
                var longitude = Console.ReadLine();
                await getData(latitude, longitude);

            }
        }
    }

        
}