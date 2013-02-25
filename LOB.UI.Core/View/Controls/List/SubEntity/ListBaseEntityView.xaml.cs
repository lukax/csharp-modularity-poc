using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.List.Base;

namespace LOB.UI.Core.View.Controls.List.SubEntity
{
    [Export]
    public partial class ListBaseEntityView : UserControl
    {
        public ListBaseEntityView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public ListBaseEntityView(ListBaseEntityViewModel<BaseEntity> viewModel)
            :this()
        {
            DataContext = viewModel;
        }
    }
}
