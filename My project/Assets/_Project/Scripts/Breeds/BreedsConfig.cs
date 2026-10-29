using UnityEngine;

namespace _Project.Scripts.Breeds
{
    [CreateAssetMenu(menuName = "Game/Breeds Config")]
    public class BreedsConfig : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _breedsUrl = "https://dogapi.dog/api/v2/breeds";
        [SerializeField, Min(1)] private int _breedsCount = 10;

        #endregion

        #region Properties

        public string BreedsUrl => _breedsUrl;
        public int BreedsCount => _breedsCount;

        #endregion
    }
}
