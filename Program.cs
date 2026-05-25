using OpenWeather.Api;

namespace Program
{ 
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("This application retrieves the current weather from the Open-Meteo API for a specified location.\n");

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