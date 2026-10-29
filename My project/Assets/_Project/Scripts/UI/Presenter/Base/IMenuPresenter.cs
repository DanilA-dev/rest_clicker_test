using _Project.Scripts.UI.Model;

namespace _Project.Scripts.UI.Presenter
{
    public interface IMenuPresenter
    {
        public MenuId Id { get; }
        public void Activate();
        public void Deactivate();
    }
}