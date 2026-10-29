using UnityEngine;

namespace _Project.Scripts.UI.View
{
    public class UIViewContainer : MonoBehaviour
    {
        #region Fields

        [SerializeField] private RectTransform _menus;

        #endregion

        #region Properties

        public RectTransform Menus => _menus;

        #endregion
    }
}
