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
using LOB.UI.Core.ViewModel.Controls.List;
using LOB.UI.Interface;
using LOB.UI.Interface.ViewModel.Base;

namespace LOB.UI.Core.View.Controls.List
{
    /// <summary>
    /// Interaction logic for ListCommandView.xaml
    /// </summary>
    public partial class ListCommandView : UserControl, IBaseView
    {
        public ListCommandView(IListCommandViewModel viewModel)
        {
            InitializeComponent();
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IListCommandViewModel; }
            set { DataContext = value; }
        }

        public string Header { get { return "Commands"; } set{} }
        public int? Index { get; set; }
        public void InitializeServices()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}
