#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity
{
    [Export]
    public partial class AlterPayCheckView : UserControl
    {
        [ImportingConstructor]
        public AlterPayCheckView()
        {
            InitializeComponent();
        }
    }
}