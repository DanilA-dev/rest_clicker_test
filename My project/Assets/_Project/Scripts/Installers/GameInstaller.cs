using _Project.Scripts.Breeds;
using _Project.Scripts.Clicker;
using _Project.Scripts.CurrencySystem;
using _Project.Scripts.Network;
using _Project.Scripts.Weather;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class GameInstaller : MonoInstaller
    {
        #region Fields

        [SerializeField] private CurrencyConfig _currencyConfig;
        [SerializeField] private ClickerConfig _clickerConfig;
        [SerializeField] private WeatherConfig _weatherConfig;
        [SerializeField] private BreedsConfig _breedsConfig;

        #endregion

        public override void InstallBindings()
        {
            Container.BindInstance(_currencyConfig);
            Container.BindInstance(_clickerConfig);
            Container.BindInstance(_weatherConfig);
            Container.BindInstance(_breedsConfig);

            Container.BindInterfacesTo<RequestQueue>().AsSingle();

            Container.Bind<CurrencyModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<ClickerService>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeatherProvider>().AsSingle();
            Container.Bind<BreedsProvider>().AsSingle();
        }
    }
}
