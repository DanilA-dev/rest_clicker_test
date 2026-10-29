using System;
using System.Collections.Generic;
using System.Threading;
using _Project.Scripts.Network;
using _Project.Scripts.Network.Requests;
using _Project.Scripts.Weather.Dto;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Weather
{
    public class WeatherProvider : IDisposable
    {
        #region Fields

        private readonly WeatherConfig _config;
        private readonly IRequestQueue _queue;
        private readonly Dictionary<string, string> _headers;
        private readonly Dictionary<string, Sprite> _icons = new();

        #endregion

        #region Constructor

        public WeatherProvider(WeatherConfig config, IRequestQueue queue)
        {
            _config = config;
            _queue = queue;
            _headers = new Dictionary<string, string> { { "User-Agent", config.UserAgent } };
        }

        #endregion

        #region Public

        public async UniTask<WeatherForecast> GetTodayAsync(CancellationToken token)
        {
            var dto = await _queue.Enqueue(new JsonRequest<ForecastResponseDto>(_config.ForecastUrl, _headers), token);

            var periods = dto?.properties?.periods;
            if (periods == null || periods.Length == 0)
                throw new InvalidOperationException("Forecast response has no periods");

            var today = periods[0];
            var icon = await GetIconAsync(today.icon, token);
            return new WeatherForecast(today.temperature, today.temperatureUnit, icon);
        }

        #endregion

        #region Private

        private async UniTask<Sprite> GetIconAsync(string url, CancellationToken token)
        {
            if (string.IsNullOrEmpty(url))
                return null;

            if (_icons.TryGetValue(url, out var cached))
                return cached;

            var texture = await _queue.Enqueue(new TextureRequest(url, _headers), token);

            if (_icons.TryGetValue(url, out cached))
            {
                Object.Destroy(texture);
                return cached;
            }

            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            _icons[url] = sprite;
            return sprite;
        }

        #endregion

        #region Disposable

        public void Dispose()
        {
            foreach (var sprite in _icons.Values)
            {
                Object.Destroy(sprite.texture);
                Object.Destroy(sprite);
            }

            _icons.Clear();
        }

        #endregion
    }
}
