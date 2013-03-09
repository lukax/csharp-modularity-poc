#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity
{
    [Export]
    public partial class AlterPhoneNumberView : UserControl, IView
    {
        private string _header;

        public AlterPhoneNumberView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public AlterPhoneNumberView(AlterPhoneNumberViewModel viewModel)
            : this()
        {
            ViewModel = viewModel;
        }

        public AlterPhoneNumberViewModel ViewModel
        {
            set { this.DataContext = value; }
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Clientes" : _header; }
            set { _header = value; }
        }

        public int? Index { get; set; }

        public void InitializeServices()
        {
        }

        public void Refresh()
        {
        }
    }
}