/*
 * This is the main script for our application. 
 * 
 * (C) Eric J. Drewitz 2026
 */

// Importing the necessary namespaces for the main function of our application. 
using OpenWeather.Api;
using OpenWeather.Calc;

// Our main program namespace
namespace Program
{ 
    class Program
    {
        // Our main task in our application
        public static async Task Main(string[] args)
        {
            Console.WriteLine("This application retrieves the current weather from the Open-Meteo API for a specified location.\n");
            Console.WriteLine("(C) Eric J. Drewitz 2026\n");
            // Continuous loop until the user manually exits the command prompt. 
            while (true)
            {
                Console.WriteLine($"Enter a latitude");
                var latitude = Console.ReadLine();
                Console.WriteLine($"Enter a longitude");
                var longitude = Console.ReadLine();

                try
                {
                    var data = await WeatherApi.GetData(latitude, longitude);

                    // Print the time and all the data attributes to the console. 
                    Console.WriteLine($"\nTime: {data.localTime}\n");
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
                catch
                {
                    throw new Exception("An Error Occurred. Please check your latitude and longitude inputs and try again.");
                }
                

            }
        }
    }
}