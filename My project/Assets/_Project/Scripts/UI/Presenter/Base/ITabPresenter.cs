using _Project.Scripts.UI.Model;

namespace _Project.Scripts.UI.Presenter
{
    public interface ITabPresenter
    {
        public TabId Id { get; }
        public void Activate();
        public void Deactivate();
    }
}