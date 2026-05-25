/*
 * This is the main script for our application. 
 * 
 * (C) Eric J. Drewitz 2026
 */

// We are importing the namespace WeatherApi from OpenWeather.Api
using OpenWeather.Api;

// Our main program namespace
namespace Program
{ 
    class Program
    {
        // Our main task in our application
        public static async Task Main(string[] args)
        {
            Console.WriteLine("This application retrieves the current weather from the Open-Meteo API for a specified location.\n");
            // Continuous loop until the user manually exits the command prompt. 
            while (true)
            {
                Console.WriteLine($"Enter a latitude");
                var latitude = Console.ReadLine();
                Console.WriteLine($"Enter a longitude");
                var longitude = Console.ReadLine();
                await WeatherApi.GetData(latitude, longitude);

            }
        }
    }
}