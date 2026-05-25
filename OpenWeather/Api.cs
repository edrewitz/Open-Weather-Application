using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using OpenWeather.Calc;

public class currentWeather
{
    public string time { get; set; }
    public double temperature_2m { get; set; }
    public double relative_humidity_2m { get; set; }
    public double apparent_temperature { get; set; }
    public double precipitation { get; set; }
    public double snowfall { get; set; }
    public double cloud_cover { get; set; }
    public double pressure_msl { get; set; }
    public double surface_pressure { get; set; }
    public double wind_speed_10m { get; set; }
    public double wind_direction_10m { get; set; }
    public double wind_gusts_10m { get; set; }
}

namespace OpenWeather.Api
{
    public static class WeatherApi
    {
        public static async Task GetData(string latitude, string longitude)
        {
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,relative_humidity_2m,apparent_temperature,precipitation,snowfall,cloud_cover,pressure_msl,surface_pressure,wind_speed_10m,wind_direction_10m,wind_gusts_10m";

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var root = JsonDocument.Parse(jsonString).RootElement;

            if (!root.TryGetProperty("current", out var currentWeatherElement))
            {
                Console.WriteLine("Response JSON does not contain a 'current' property.");
                return;
            }

            var data = JsonSerializer.Deserialize<currentWeather>(currentWeatherElement.GetRawText());
            if (data == null)
            {
                Console.WriteLine("Unable to parse current weather data.");
                return;
            }

            var time = data.time;
            var dateTime = DateTime.Parse(time);
            var localTime = dateTime.ToLocalTime();
            Console.WriteLine($"\nTime: {localTime}\n");
            Console.WriteLine($"2-Meter Temperature: {Math.Round(unitConversion.toFahrenheit(data.temperature_2m))}°F");
            Console.WriteLine($"2-Meter Relative Humidity: {data.relative_humidity_2m}%");
            Console.WriteLine($"Apparent Temperature: {Math.Round(unitConversion.toFahrenheit(data.apparent_temperature))}°F");
            Console.WriteLine($"Precipitation: {Math.Round(unitConversion.toInches(data.precipitation), 2)} in");
            Console.WriteLine($"Snowfall: {Math.Round(unitConversion.toInches(data.snowfall), 2)} in");
            Console.WriteLine($"Cloud Cover: {data.cloud_cover}%");
            Console.WriteLine($"Pressure (Reduced to MSL): {data.pressure_msl} hPa");
            Console.WriteLine($"Surface Pressure: {data.surface_pressure} hPa");
            Console.WriteLine($"10-Meter Wind Speed: {Math.Round(unitConversion.toMph(data.wind_speed_10m))} mph");
            Console.WriteLine($"10-Meter Wind Direction: {data.wind_direction_10m}°");
            Console.WriteLine($"10-Meter Wind Gusts: {Math.Round(unitConversion.toMph(data.wind_gusts_10m))} mph\n");
        }
    }
}