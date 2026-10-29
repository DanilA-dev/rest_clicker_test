using System;
using _Project.Scripts.TweenAnimations;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI.View.Items
{
    public class ClickPopupView : MonoBehaviour
    {
        #region Fields

        [SerializeField] private TMP_Text _text;
        [SerializeField] private TweenPlayable _tweenPlayable;

        private Action _onComplete;

        #endregion

        #region Monobehaviour

        private void Awake() => _tweenPlayable.OnComplete += HandleComplete;

        private void OnDestroy() => _tweenPlayable.OnComplete -= HandleComplete;

        #endregion

        #region Public

        public void Setup(int amount, Vector2 localPosition)
        {
            _text.text = $"+{amount}";
            transform.localPosition = localPosition;
        }

        public void Play(Action onComplete)
        {
            _onComplete = onComplete;
            _tweenPlayable.Play();
        }

        #endregion

        #region Private

        private void HandleComplete()
        {
            var callback = _onComplete;
            _onComplete = null;
            callback?.Invoke();
        }

        #endregion

        #region Pool

        public class Pool : MonoMemoryPool<int, Vector2, ClickPopupView>
        {
            protected override void Reinitialize(int amount, Vector2 localPosition, ClickPopupView item)
            {
                item.Setup(amount, localPosition);
            }

            protected override void OnDespawned(ClickPopupView item)
            {
                item._tweenPlayable.Kill();
                item._onComplete = null;
                base.OnDespawned(item);
            }
        }

        #endregion
    }
}
