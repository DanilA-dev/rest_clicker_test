using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.CurrencySystem
{
    [System.Serializable]
    public class Currency
    {
        #region Fields

        [SerializeField] private CurrencyType _type;
        [Space]
        [SerializeField] private long _defaultValue;
        [SerializeField] private bool _hasMaxValue;
        [ShowIf(nameof(_hasMaxValue))]
        [SerializeField] private long _maxValue;

        private ReactiveProperty<long> _value;

        #endregion

        #region Properties

        public CurrencyType CurrencyType => _type;

        public IReadOnlyReactiveProperty<long> Value => ValueProperty;

        public bool HasMaxValue
        {
            get => _hasMaxValue;
            set => _hasMaxValue = value;
        }

        public long MaxValue
        {
            get => _maxValue;
            set => _maxValue = value;
        }

        public long DefaultValue
        {
            get => _defaultValue;
            set => _defaultValue = value;
        }

        private ReactiveProperty<long> ValueProperty => _value ??= new ReactiveProperty<long>(_defaultValue);

        #endregion

        #region Constructors

        public Currency(CurrencyType currencyType, long initialValue)
        {
            _type = currencyType;
            _defaultValue = initialValue;
        }

        #endregion

        #region Public

        public bool TryDeposit(long depositValue)
        {
            var current = ValueProperty.Value;
            if (depositValue <= 0 || _hasMaxValue && current >= _maxValue)
                return false;

            var newValue = current + depositValue;
            ValueProperty.Value = _hasMaxValue && newValue >= _maxValue ? _maxValue : newValue;
            return true;
        }

        public bool TryWithdraw(long withdrawValue)
        {
            if (withdrawValue <= 0 || ValueProperty.Value < withdrawValue)
                return false;

            ValueProperty.Value -= withdrawValue;
            return true;
        }

        public bool TrySet(long value)
        {
            if (value < 0 || _hasMaxValue && value > _maxValue)
                return false;

            ValueProperty.Value = value;
            return true;
        }

        public void ResetToDefault() => ValueProperty.Value = _defaultValue;

        #endregion
    }
}
