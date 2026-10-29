using UnityEngine;

namespace _Project.Scripts.Weather
{
    public class WeatherForecast
    {
        #region Properties

        public int Temperature { get; }
        public string TemperatureUnit { get; }
        public Sprite Icon { get; }

        #endregion

        #region Constructor

        public WeatherForecast(int temperature, string temperatureUnit, Sprite icon)
        {
            Temperature = temperature;
            TemperatureUnit = temperatureUnit;
            Icon = icon;
        }

        #endregion
    }
}
