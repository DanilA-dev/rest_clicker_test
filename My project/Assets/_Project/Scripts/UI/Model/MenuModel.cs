using UniRx;

namespace _Project.Scripts.UI.Model
{
    public class MenuModel
    {
        #region Properties

        public ReactiveProperty<MenuId> CurrentTab { get; } = new(MenuId.Clicker);

        #endregion
    }
}