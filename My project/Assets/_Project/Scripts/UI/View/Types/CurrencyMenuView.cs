using System.Collections.Generic;
using _Project.Scripts.UI.View.Items;
using UnityEngine;

namespace _Project.Scripts.UI.View.Types
{
    public class CurrencyMenuView : BaseMenuView
    {
        #region Fields

        [SerializeField] private CurrencyItemView[] _items;

        #endregion

        #region Properties

        public IReadOnlyList<CurrencyItemView> Items => _items;

        #endregion
    }
}
