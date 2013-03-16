#region Usings

using System.Windows.Controls;
using LOB.UI.Interface;
using LOB.UI.Interface.ViewModel.Base;

#endregion

namespace LOB.UI.Core.View.Controls.Main
{
    /// <summary>
    ///     Interaction logic for MessageToolsView.xaml
    /// </summary>
    public partial class MessageToolsView : UserControl, IBaseView
    {
        public MessageToolsView()
        {
            InitializeComponent();
        }

        public IBaseViewModel ViewModel { get { return this.DataContext as IBaseViewModel; } set { this.DataContext = value; } }
        public string Header { get; set; }
        public int? Index { get; set; }
        public void InitializeServices()
        {
        }

        public void Refresh()
        {
        }
    }
}