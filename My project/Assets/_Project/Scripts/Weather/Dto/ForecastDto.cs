using System;

namespace _Project.Scripts.Weather.Dto
{

    [Serializable]
    public class ForecastResponseDto
    {
        public ForecastPropertiesDto properties;
    }

    [Serializable]
    public class ForecastPropertiesDto
    {
        public ForecastPeriodDto[] periods;
    }

    [Serializable]
    public class ForecastPeriodDto
    {
        public string name;
        public int temperature;
        public string temperatureUnit;
        public string icon;
    }
}
