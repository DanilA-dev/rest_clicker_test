using _Project.Scripts.CurrencySystem;
using _Project.Scripts.TweenAnimations;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.View.Items
{
    public class CurrencyItemView : MonoBehaviour
    {
        #region Fields

        [SerializeField] private CurrencyType _type;
        [SerializeField] private TMP_Text _amount;

        [FoldoutGroup("Animations")] 
        [SerializeField] private TweenPlayable _tweenPlayable;

        #endregion

        #region Properties

        public CurrencyType Type => _type;

        #endregion

        #region Public

        public void SetAmount(long value)
        {
            _amount.text = value.ToString();
            _tweenPlayable?.Play();
        }

        #endregion
    }
}
