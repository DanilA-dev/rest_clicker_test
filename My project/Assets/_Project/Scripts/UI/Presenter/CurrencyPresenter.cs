using System;
using _Project.Scripts.CurrencySystem;
using _Project.Scripts.UI.View.Types;
using UniRx;
using Zenject;

namespace _Project.Scripts.UI.Presenter
{
    public class CurrencyPresenter : IInitializable, IDisposable
    {
        #region Fields

        private readonly CurrencyMenuView _view;
        private readonly CurrencyModel _model;
        private readonly CompositeDisposable _disposables = new();

        #endregion

        #region Constructor

        public CurrencyPresenter(CurrencyMenuView view, CurrencyModel model)
        {
            _view = view;
            _model = model;
        }

        #endregion

        #region Lifecycle

        public void Initialize()
        {
            foreach (var item in _view.Items)
            {
                var currency = _model.Get(item.Type);

                currency.Value
                    .Subscribe(value =>
                    {
                        item.SetAmount(value);
                    })
                    .AddTo(_disposables);
            }
        }

        public void Dispose() => _disposables.Dispose();

        #endregion
    }
}
