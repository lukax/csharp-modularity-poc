#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using LOB.UI.Core.ViewModel.Controls.Alter;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.Alter
{
    [Export]
    public partial class AlterClientView : UserControl, ITabProp, IView
    {
        private string _header;

        [ImportingConstructor]
        public AlterClientView(AlterClientViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            Messenger.Default.Register<object>(DataContext, "SaveChangesCommand", o => Messenger.Default.Send("Cancel"));
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Alterar Cliente" : _header; }
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