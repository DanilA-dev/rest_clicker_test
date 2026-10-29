using System;
using _Project.Scripts.UI.Model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.View.Tabs
{
    public class TabButtonView : MonoBehaviour
    {
        #region Fields

        [SerializeField] private MenuId _id;
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _selectedFrame;

        #endregion

        #region Properties

        public MenuId Id => _id;
        public IObservable<MenuId> OnClick => _button.OnClickAsObservable().Select(_ => _id);

        #endregion

        #region Public

        public void SetSelected(bool isSelected)
        {
            if (_selectedFrame != null)
                _selectedFrame.SetActive(isSelected);
        }

        #endregion
    }
}
