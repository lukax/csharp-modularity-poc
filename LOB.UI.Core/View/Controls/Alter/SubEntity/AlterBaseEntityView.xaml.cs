#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity
{
    [Export]
    public partial class AlterBaseEntityView : UserControl
    {
        public AlterBaseEntityView()
        {
            InitializeComponent();    
        }

        [ImportingConstructor]
        public AlterBaseEntityView(AlterBaseEntityViewModel<BaseEntity> viewModel )
            :this()
        {
            DataContext = viewModel;
        }
    }
}