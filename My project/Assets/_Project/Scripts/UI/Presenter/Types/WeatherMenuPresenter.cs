using System;
using System.Threading;
using _Project.Scripts.UI.Model;
using _Project.Scripts.UI.View.Types;
using _Project.Scripts.Weather;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.UI.Presenter.Types
{
    public class WeatherMenuPresenter : BaseMenuPresenter<WeatherMenuView>
    {
        #region Fields

        private readonly WeatherProvider _weather;
        private readonly WeatherConfig _config;

        #endregion

        #region Properties

        public override MenuId Id => MenuId.Weather;

        #endregion

        #region Constructor

        public WeatherMenuPresenter(WeatherMenuView view, WeatherProvider weather, WeatherConfig config) : base(view)
        {
            _weather = weather;
            _config = config;
        }

        #endregion

        #region Menu

        protected override void OnActivate(CancellationToken token) => PollAsync(token).Forget();

        protected override void OnDeactivate()
        {
        }

        #endregion

        #region Private

        private async UniTaskVoid PollAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                RequestForecastAsync(token).Forget();

                var cancelled = await UniTask.Delay(TimeSpan.FromSeconds(_config.PollInterval), cancellationToken: token)
                    .SuppressCancellationThrow();
                if (cancelled)
                    return;
            }
        }

        private async UniTaskVoid RequestForecastAsync(CancellationToken token)
        {
            try
            {
                View.OnLoad();
                
                var forecast = await _weather.GetTodayAsync(token);
                if (token.IsCancellationRequested)
                    return;

                View.OnReady();
                View.SetForecast(forecast.Icon, forecast.Temperature, forecast.TemperatureUnit);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Weather request failed: {e.Message}");
            }
        }

        #endregion
    }
}
