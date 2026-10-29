using UniRx;

namespace _Project.Scripts.UI.Model
{
    public class MenuModel
    {
        public ReactiveProperty<MenuId> CurrentTab { get; } = new(MenuId.Clicker);
    }
}