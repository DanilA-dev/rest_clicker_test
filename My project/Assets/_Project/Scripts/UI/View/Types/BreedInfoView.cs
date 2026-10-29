using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.View.Types
{
    public class BreedInfoView : BaseMenuView
    {
        #region Fields

        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Button _okButton;
        [SerializeField] private RectTransform _container;

        #endregion

        #region Properties

        public IObservable<Unit> OnOkClick => _okButton.OnClickAsObservable();

        #endregion

        #region Public

        public void Init(string title, string description)
        {
            _title.text = title;
            _description.text = description;
        }

        public override void Show()
        {
            transform.SetAsLastSibling();
            base.Show();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_container);
        }

        #endregion
    }
}
