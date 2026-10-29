using UnityEngine;

namespace _Project.Scripts.Utility
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeArea : MonoBehaviour
    {
        #region Fields

        [SerializeField] private bool _conformX = true;
        [SerializeField] private bool _conformY = true;

        private RectTransform _rectTransform;
        private Rect _lastSafeArea;
        private Vector2Int _lastScreenSize;

        #endregion

        #region Monobehaviour

        private void Awake() => _rectTransform = (RectTransform)transform;

        private void OnEnable() => Apply();

        private void Update()
        {
            if (Screen.safeArea != _lastSafeArea || Screen.width != _lastScreenSize.x || Screen.height != _lastScreenSize.y)
                Apply();
        }

        #endregion

        #region Private

        private void Apply()
        {
            var safeArea = Screen.safeArea;
            _lastSafeArea = safeArea;
            _lastScreenSize = new Vector2Int(Screen.width, Screen.height);

            if (Screen.width <= 0 || Screen.height <= 0)
                return;

            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            if (!_conformX)
            {
                anchorMin.x = 0;
                anchorMax.x = 1;
            }

            if (!_conformY)
            {
                anchorMin.y = 0;
                anchorMax.y = 1;
            }

            _rectTransform.anchorMin = anchorMin;
            _rectTransform.anchorMax = anchorMax;
            _rectTransform.offsetMin = Vector2.zero;
            _rectTransform.offsetMax = Vector2.zero;
        }

        #endregion
    }
}
