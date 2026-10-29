using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.UI.Model;
using _Project.Scripts.UI.View.Tabs;
using UniRx;
using Zenject;

namespace _Project.Scripts.UI.Presenter
{
    public class MenusPresenter : IInitializable, IDisposable
    {
        #region Fields

        private readonly TabsMenuView _tabsMenuView;
        private readonly MenuModel _model;
        private readonly Dictionary<MenuId, IMenuPresenter> _menus;
        private readonly CompositeDisposable _disposables = new();

        private IMenuPresenter _current;

        #endregion

        #region Constructor

        public MenusPresenter(TabsMenuView tabsMenuView, MenuModel model, List<IMenuPresenter> menus)
        {
            _tabsMenuView = tabsMenuView;
            _model = model;
            _menus = menus.ToDictionary(m => m.Id);
        }

        #endregion

        #region Lifecycle

        public void Initialize()
        {
            foreach (var menu in _menus.Values)
                menu.Deactivate();

            _tabsMenuView.OnTabClicked
                .Subscribe(id => _model.CurrentTab.Value = id)
                .AddTo(_disposables);

            _model.CurrentTab
                .Subscribe(OnMenuChanged)
                .AddTo(_disposables);
        }

        public void Dispose() => _disposables.Dispose();

        #endregion

        #region Private

        private void OnMenuChanged(MenuId id)
        {
            if (!_menus.TryGetValue(id, out var next))
                return;

            _current?.Deactivate();
            _current = next;
            _current.Activate();
            _tabsMenuView.SetSelected(id);
        }

        #endregion
    }
}
