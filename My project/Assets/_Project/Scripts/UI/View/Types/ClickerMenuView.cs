using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _Project.Scripts.UI.View.Types
{
    public class ClickerMenuView : BaseMenuView
    {
        #region Fields

        [SerializeField] private Button _clickButton;
        [SerializeField] private RectTransform _popupContainer;

        #endregion

        #region Properties

        public IObservable<Unit> OnClick => _clickButton.OnClickAsObservable();

        public RectTransform PopupContainer => _popupContainer;

        #endregion

        #region Public

        public Vector2 GetRandomPopupPosition()
        {
            var rect = _popupContainer.rect;
            return new Vector2(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMin, rect.yMax));
        }

        #endregion
    }
}
