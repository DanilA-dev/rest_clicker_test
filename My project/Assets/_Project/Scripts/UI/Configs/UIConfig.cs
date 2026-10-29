using System.Collections.Generic;
using _Project.Scripts.UI.View;
using _Project.Scripts.UI.View.Items;
using UnityEngine;

namespace _Project.Scripts.UI.Configs
{
    [CreateAssetMenu(menuName = "Game/UI Config")]
    public class UIConfig : ScriptableObject
    {
        #region Fields

        [SerializeField] private UIViewContainer _viewContainerPrefab;
        [SerializeField] private List<BaseMenuView> _menus = new();

        [Header("Click Popup")]
        [SerializeField] private ClickPopupView _clickPopupPrefab;
        [SerializeField, Min(0)] private int _clickPopupPoolSize = 10;

        [Header("Breeds")]
        [SerializeField] private BreedItemView _breedItemPrefab;
        [SerializeField, Min(0)] private int _breedItemPoolSize = 10;

        #endregion

        #region Properties

        public UIViewContainer ViewContainerPrefab => _viewContainerPrefab;
        public IReadOnlyList<BaseMenuView> Menus => _menus;
        public ClickPopupView ClickPopupPrefab => _clickPopupPrefab;
        public int ClickPopupPoolSize => _clickPopupPoolSize;
        public BreedItemView BreedItemPrefab => _breedItemPrefab;
        public int BreedItemPoolSize => _breedItemPoolSize;

        #endregion
    }
}
