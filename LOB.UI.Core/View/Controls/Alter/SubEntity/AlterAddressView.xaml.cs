#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity
{
    [Export]
    public partial class AlterAddressView : UserControl, ITabProp, IView
    {
        private string _header;

        [ImportingConstructor]
        public AlterAddressView() {
            InitializeComponent();
        }

        public string Header {
            get { return (string.IsNullOrEmpty(_header)) ? "Clientes" : _header; }
            set { _header = value; }
        }

        public int? Index { get; set; }

        public void InitializeServices() {
        }

        public void Refresh() {
        }
    }
}