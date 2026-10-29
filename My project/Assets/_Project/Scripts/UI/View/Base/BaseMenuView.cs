using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace _Project.Scripts.UI.View
{
    public abstract class BaseMenuView : MonoBehaviour
    {
        #region Fields

        [FoldoutGroup("Events"), PropertyOrder(100)] 
        [SerializeField] private UnityEvent _onLoad;
        [FoldoutGroup("Events"), PropertyOrder(100)] 
        [SerializeField] private UnityEvent _onReady;

        private bool _isLoaded;
        
        #endregion

        #region Public

        public virtual void Show() => gameObject.SetActive(true);
        public virtual void Hide()
        {
            gameObject.SetActive(false);
            _isLoaded = false;
        }

        public virtual void OnLoad()
        {
            if(_isLoaded)
                return;
            
            _onLoad?.Invoke();
        }

        public virtual void OnReady()
        {
            if(_isLoaded)
                return;
            
            _onReady?.Invoke();
            _isLoaded = true;
        }

        #endregion
    }
}
