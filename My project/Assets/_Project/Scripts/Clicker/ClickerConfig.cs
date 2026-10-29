using UnityEngine;

namespace _Project.Scripts.Clicker
{
    [CreateAssetMenu(menuName = "Game/Clicker Config")]
    public class ClickerConfig : ScriptableObject
    {
        #region Fields

        [Header("Click")]
        [SerializeField, Min(1)] private int _clickReward = 1;
        [SerializeField, Min(1)] private int _clickEnergyCost = 1;

        [Header("Auto Click")]
        [SerializeField, Min(0.1f)] private float _autoClickInterval = 3f;

        [Header("Energy Regen")]
        [SerializeField, Min(0.1f)] private float _energyRegenInterval = 10f;
        [SerializeField, Min(1)] private int _energyRegenAmount = 10;

        #endregion

        #region Properties

        public int ClickReward => _clickReward;
        public int ClickEnergyCost => _clickEnergyCost;
        public float AutoClickInterval => _autoClickInterval;
        public float EnergyRegenInterval => _energyRegenInterval;
        public int EnergyRegenAmount => _energyRegenAmount;

        #endregion
    }
}
