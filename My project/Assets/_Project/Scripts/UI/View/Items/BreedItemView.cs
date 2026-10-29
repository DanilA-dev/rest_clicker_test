using System;
using Sirenix.OdinInspector;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI.View.Items
{
    public class BreedItemView : MonoBehaviour
    {
        #region Fields

        [SerializeField] private TMP_Text _title;
        [SerializeField] private Button _button;

        [FoldoutGroup("Events")]
        [SerializeField] private UnityEvent _onLoad;
        [FoldoutGroup("Events")]
        [SerializeField] private UnityEvent _onReady;

        #endregion

        #region Properties

        public IObservable<Unit> OnClick => _button.OnClickAsObservable();

        #endregion

        #region Public

        public void Setup(string title)
        {
            _title.text = title;
            SetLoading(false);
        }

        public void SetLoading(bool isLoading)
        {
            if (isLoading)
                _onLoad?.Invoke();
            else
                _onReady?.Invoke();
        }

        #endregion

        #region Pool

        public class Pool : MonoMemoryPool<string, BreedItemView>
        {
            protected override void Reinitialize(string title, BreedItemView item)
            {
                item.Setup(title);
                item.transform.SetAsLastSibling();
            }
        }

        #endregion
    }
}
