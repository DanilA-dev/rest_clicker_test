using _Project.Scripts.UI.Model;
using _Project.Scripts.UI.View.Types;

namespace _Project.Scripts.UI.Presenter.Types
{
    public class ClickerTab : BaseTab<ClickerTabView>
    {
        #region Properties

        public override TabId Id => TabId.Clicker;

        #endregion
        
        #region Constructor

        public ClickerTab(ClickerTabView view) : base(view)
        {
        }

        #endregion

        #region Tab

        protected override void OnActivate()
        {
        }

        protected override void OnDeactivate()
        {
        }

        #endregion
    }
}