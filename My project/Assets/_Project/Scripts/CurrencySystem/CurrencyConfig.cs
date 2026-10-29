using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.CurrencySystem
{
    [CreateAssetMenu(menuName = "Game/Currency Config")]
    public class CurrencyConfig : ScriptableObject
    {
        #region Fields

        [SerializeField] private List<Currency> _currencies = new();

        #endregion

        #region Properties

        public IReadOnlyList<Currency> Currencies => _currencies;

        #endregion
    }
}
