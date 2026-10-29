using System.Collections.Generic;
using System.Linq;

namespace _Project.Scripts.CurrencySystem
{
    public class CurrencyModel
    {
        #region Fields

        private readonly Dictionary<CurrencyType, Currency> _currencies;

        #endregion

        #region Constructor

        public CurrencyModel(CurrencyConfig config)
        {
            _currencies = config.Currencies.ToDictionary(c => c.CurrencyType, c => new Currency(c.CurrencyType, c.DefaultValue)
            {
                HasMaxValue = c.HasMaxValue,
                MaxValue = c.MaxValue
            });
        }

        #endregion

        #region Public

        public Currency Get(CurrencyType type) => _currencies[type];

        #endregion
    }
}
