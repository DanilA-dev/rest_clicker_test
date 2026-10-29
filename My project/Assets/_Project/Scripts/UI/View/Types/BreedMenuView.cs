using UnityEngine;

namespace _Project.Scripts.UI.View.Types
{
    public class BreedMenuView : BaseMenuView
    {
        #region Fields

        [SerializeField] private RectTransform _content;

        #endregion

        #region Properties

        public RectTransform Content => _content;

        #endregion
    }
}
