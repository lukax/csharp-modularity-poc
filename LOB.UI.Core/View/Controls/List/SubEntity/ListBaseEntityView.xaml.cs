using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.List.Base;

namespace LOB.UI.Core.View.Controls
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
