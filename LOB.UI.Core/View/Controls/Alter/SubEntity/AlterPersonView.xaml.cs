using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace LOB.UI.Core.View.Controls.Alter.SubEntity
{
    [Export]
    public partial class AlterPersonView : UserControl
    {
        [ImportingConstructor]
        public AlterPersonView()
        {
            InitializeComponent();
        }
    }
}
