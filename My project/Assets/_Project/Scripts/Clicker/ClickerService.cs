using System;
using _Project.Scripts.CurrencySystem;
using UniRx;
using Zenject;

namespace _Project.Scripts.Clicker
{
    public class ClickerService : IInitializable, IDisposable
    {
        #region Fields

        private readonly ClickerConfig _config;
        private readonly Currency _coins;
        private readonly Currency _energy;
        private readonly Subject<int> _clicked = new();
        private readonly CompositeDisposable _disposables = new();

        #endregion

        #region Properties

        public IObservable<int> OnClicked => _clicked;

        #endregion

        #region Constructor

        public ClickerService(ClickerConfig config, CurrencyModel currencies)
        {
            _config = config;
            _coins = currencies.Get(CurrencyType.Coins);
            _energy = currencies.Get(CurrencyType.Energy);
        }

        #endregion

        #region Lifecycle

        public void Initialize()
        {
            Observable.Interval(TimeSpan.FromSeconds(_config.AutoClickInterval))
                .Subscribe(_ => TryClick())
                .AddTo(_disposables);

            Observable.Interval(TimeSpan.FromSeconds(_config.EnergyRegenInterval))
                .Subscribe(_ => _energy.TryDeposit(_config.EnergyRegenAmount))
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
            _clicked.Dispose();
        }

        #endregion

        #region Public

        public bool TryClick()
        {
            if (!_energy.TryWithdraw(_config.ClickEnergyCost))
                return false;

            _coins.TryDeposit(_config.ClickReward);
            _clicked.OnNext(_config.ClickReward);
            return true;
        }

        #endregion
    }
}
