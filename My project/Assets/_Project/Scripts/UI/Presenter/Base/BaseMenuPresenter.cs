using System;
using System.Threading;
using _Project.Scripts.UI.Model;
using _Project.Scripts.UI.View;

namespace _Project.Scripts.UI.Presenter
{
    public abstract class BaseMenuPresenter<TView> : IMenuPresenter, IDisposable
    where TView : BaseMenuView
    {
        #region Fields

        private CancellationTokenSource _cToken;

        #endregion

        #region Properties

        public abstract MenuId Id { get; }

        protected TView View { get; }

        #endregion

        #region Constructor

        protected BaseMenuPresenter(TView view)
        {
            View = view;
        }

        #endregion

        #region Public

        public void Activate()
        {
            Cancel();
            _cToken = new CancellationTokenSource();
            View.Show();
            OnActivate(_cToken.Token);
        }

        public void Deactivate()
        {
            Cancel();
            View.Hide();
            OnDeactivate();
        }

        #endregion

        #region Abstract

        protected abstract void OnActivate(CancellationToken token);
        protected abstract void OnDeactivate();

        #endregion

        #region Private

        private void Cancel()
        {
            _cToken?.Cancel();
            _cToken?.Dispose();
            _cToken = null;
        }

        #endregion

        #region Disposable

        public void Dispose() => Cancel();

        #endregion
    }
}
