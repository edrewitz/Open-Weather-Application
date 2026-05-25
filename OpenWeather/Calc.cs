using System;

namespace OpenWeather.Calc
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

        public static double toInches(double precipitationMm)
        {
            return (precipitationMm * 0.0393701);
        }
    }

}

