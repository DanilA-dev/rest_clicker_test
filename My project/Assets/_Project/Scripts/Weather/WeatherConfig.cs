using UnityEngine;

namespace _Project.Scripts.Weather
{
    [CreateAssetMenu(menuName = "Game/Weather Config")]
    public class WeatherConfig : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _forecastUrl = "https://api.weather.gov/gridpoints/TOP/32,81/forecast";
        [SerializeField] private string _userAgent;
        [SerializeField, Min(0.1f)] private float _pollInterval = 5f;

        #endregion

        #region Properties

        public string ForecastUrl => _forecastUrl;
        public string UserAgent => _userAgent;
        public float PollInterval => _pollInterval;

        #endregion
    }
}
