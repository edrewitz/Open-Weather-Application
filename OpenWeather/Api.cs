/*
 * This file hosts the functions needed to make the API call and output the data to the console. 
 * 
 * (C) Eric J. Drewitz 2026
 */

using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using OpenWeather.Calc;

// Our public class of all the data attributes for current weather. 
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

    // Added property to hold converted local time to fix CS1061
    public DateTime localTime { get; set; }
}

// Our namespace for the API call function.
namespace OpenWeather.Api
{
    public static class WeatherApi
    {
        public static async Task<currentWeather> GetData(string latitude, string longitude)
        {
            // Open-Meto API Call URL
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,relative_humidity_2m,apparent_temperature,precipitation,snowfall,cloud_cover,pressure_msl,surface_pressure,wind_speed_10m,wind_direction_10m,wind_gusts_10m";

            // Create our new HTTP Client
            using var httpClient = new HttpClient();

            // Ping the server for a response. 
            var response = await httpClient.GetAsync(url);

            // Ensure we get a successful response, otherwise throw an exception.
            response.EnsureSuccessStatusCode();

            // Read our response as a string, then parse it as JSON.
            var jsonString = await response.Content.ReadAsStringAsync();

            // Parse the JSON string and extract the "current" property, which contains the current weather data.
            var root = JsonDocument.Parse(jsonString).RootElement;

            // Check if the "current" property exists in the JSON response.
            if (!root.TryGetProperty("current", out var currentWeatherElement))
            {
                Console.WriteLine("Response JSON does not contain a 'current' property.");
                return null;
            }

            // Deserialize the "current" property into our currentWeather class. If deserialization fails, print an error message and return.
            var data = JsonSerializer.Deserialize<currentWeather>(currentWeatherElement.GetRawText());
            if (data == null)
            {
                Console.WriteLine("Unable to parse current weather data.");
                return null;
            }

            // Extract the time attribute which is in the form of a string.
            var time = data.time;

            // Convert the time string to a DateTime object.
            var dateTime = DateTime.Parse(time);

            // Convert the DateTime object to local time.
            data.localTime = dateTime.ToLocalTime();

            return data;
        }
    }
}