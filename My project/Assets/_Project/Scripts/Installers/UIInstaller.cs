using _Project.Scripts.UI.Configs;
using _Project.Scripts.UI.Model;
using _Project.Scripts.UI.Presenter;
using _Project.Scripts.UI.Presenter.Types;
using _Project.Scripts.UI.View;
using _Project.Scripts.UI.View.Items;
using _Project.Scripts.UI.View.Tabs;
using _Project.Scripts.UI.View.Types;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class UIInstaller : MonoInstaller
    {
        #region Fields

        [SerializeField] private UIConfig _uiConfig;

        #endregion

        #region Bindings

        public override void InstallBindings()
        {
            BindViews();
            BindMenus();
        }

        #endregion

        #region Private

        private void BindViews()
        {
            CreateUIRoot();
            CreateMenuViews();
            CreatePoolForClickPopups();
            CreatePoolForBreedViews();
        }

        private void CreatePoolForBreedViews()
        {
            Container.BindMemoryPool<BreedItemView, BreedItemView.Pool>()
                .WithInitialSize(_uiConfig.BreedItemPoolSize)
                .FromComponentInNewPrefab(_uiConfig.BreedItemPrefab)
                .UnderTransform(ctx => ctx.Container.Resolve<BreedMenuView>().Content);
        }

        private void CreatePoolForClickPopups()
        {
            Container.BindMemoryPool<ClickPopupView, ClickPopupView.Pool>()
                .WithInitialSize(_uiConfig.ClickPopupPoolSize)
                .FromComponentInNewPrefab(_uiConfig.ClickPopupPrefab)
                .UnderTransform(ctx => ctx.Container.Resolve<ClickerMenuView>().PopupContainer);
        }

        private void CreateMenuViews()
        {
            foreach (var prefab in _uiConfig.Menus)
            {
                Container.Bind(prefab.GetType())
                    .FromComponentInNewPrefab(prefab)
                    .UnderTransform(ctx => ctx.Container.Resolve<UIViewContainer>().Menus)
                    .AsSingle();
            }
        }

        private void CreateUIRoot()
        {
            Container.Bind<UIViewContainer>()
                .FromComponentInNewPrefab(_uiConfig.ViewContainerPrefab)
                .AsSingle();
        }

        private void BindMenus()
        {
            Container.Bind<MenuModel>().AsSingle();

            Container.BindInterfacesTo<ClickerMenuPresenter>().AsSingle();
            Container.BindInterfacesTo<WeatherMenuPresenter>().AsSingle();
            Container.BindInterfacesTo<BreedMenuPresenter>().AsSingle();

            Container.BindInterfacesTo<MenusPresenter>().AsSingle();
            Container.BindInterfacesTo<CurrencyPresenter>().AsSingle();
        }

        #endregion
    }
}
