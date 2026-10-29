using System.Threading;
using _Project.Scripts.Clicker;
using _Project.Scripts.UI.Model;
using _Project.Scripts.UI.View.Items;
using _Project.Scripts.UI.View.Types;
using UniRx;

namespace _Project.Scripts.UI.Presenter.Types
{
    public class ClickerMenuPresenter : BaseMenuPresenter<ClickerMenuView>
    {
        #region Fields

        private readonly ClickerService _clicker;
        private readonly ClickPopupView.Pool _popupPool;
        private readonly CompositeDisposable _disposables = new();

        #endregion

        #region Properties

        public override MenuId Id => MenuId.Clicker;

        #endregion

        #region Constructor

        public ClickerMenuPresenter(ClickerMenuView view, ClickerService clicker, ClickPopupView.Pool popupPool) : base(view)
        {
            _clicker = clicker;
            _popupPool = popupPool;
        }

        #endregion

        #region Menu

        protected override void OnActivate(CancellationToken token)
        {
            View.OnClick
                .Subscribe(_ => _clicker.TryClick())
                .AddTo(_disposables);

            _clicker.OnClicked
                .Subscribe(SpawnPopup)
                .AddTo(_disposables);
        }

        protected override void OnDeactivate() => _disposables.Clear();

        #endregion

        #region Private

        private void SpawnPopup(int amount)
        {
            var popup = _popupPool.Spawn(amount, View.GetRandomPopupPosition());
            popup.Play(() => _popupPool.Despawn(popup));
        }

        #endregion
    }
}
