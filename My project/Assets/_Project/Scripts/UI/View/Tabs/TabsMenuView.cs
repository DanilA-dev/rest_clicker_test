using System;
using System.Linq;
using _Project.Scripts.UI.Model;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.UI.View.Tabs
{
    public class TabsMenuView : BaseMenuView
    {
        #region Fields

        [SerializeField] private TabButtonView[] _buttons;

        #endregion

        #region Properties

        public IObservable<MenuId> OnTabClicked => _buttons.Select(b => b.OnClick).Merge();

        #endregion

        #region Public

        public void SetSelected(MenuId id)
        {
            foreach (var button in _buttons)
                button.SetSelected(button.Id == id);
        }

        #endregion
    }
}
