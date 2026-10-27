using UniRx;

namespace _Project.Scripts.UI.Model
{
    public class TabModel
    {
        public ReactiveProperty<TabId> CurrentTab { get; } = new(TabId.Clicker);
    }
}