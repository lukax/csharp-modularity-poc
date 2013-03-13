using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LOB.UI.Interface;
using LOB.UI.Interface.ViewModel.Base;

namespace LOB.UI.Core.View.Controls.Main
{
    /// <summary>
    /// Interaction logic for ColumnToolsView.xaml
    /// </summary>
    public partial class ColumnToolsView : UserControl, IBaseView
    {
        public ColumnToolsView()
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
