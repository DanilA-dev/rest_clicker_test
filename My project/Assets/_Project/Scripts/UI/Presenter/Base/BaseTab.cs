using System;
using System.Threading;
using _Project.Scripts.UI.Model;
using _Project.Scripts.UI.View;
using Unity.VisualScripting;

namespace _Project.Scripts.UI.Presenter
{
    public abstract class BaseTab<TView> : ITabPresenter, IDisposable
    where TView : BaseTabView
    {
        #region Fields

        private readonly TView _view;
        private CancellationTokenSource _cToken;

        #endregion
        
        #region Properties

        public abstract TabId Id { get; }

        #endregion

        #region Constructor

        protected BaseTab(TView view)
        {
            _view = view;
        }

        #endregion
        
        #region Public

        public void Activate()
        {
            _cToken = new CancellationTokenSource();
            _view?.Show();
            OnActivate();
        }

        public void Deactivate()
        {
            Cancel();
            _view?.Hide();
            OnDeactivate();
        }

        #endregion

        #region Abstract


        protected abstract void OnActivate();
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