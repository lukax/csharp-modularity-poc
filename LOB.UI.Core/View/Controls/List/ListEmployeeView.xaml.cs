#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.List
{
    [Export]
    public partial class ListEmployeeView : UserControl, ITabProp, IView
    {
        [ImportingConstructor]
        public ListEmployeeView(ListBaseEntityViewModel<Employee> dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
        }

        public string Header
        {
            get { return "Funcionarios"; }
            set { }
        }

        public int? Index { get; set; }

        public void InitializeServices()
        {
            throw new System.NotImplementedException();
        }

        public void Refresh()
        {
            throw new System.NotImplementedException();
        }
    }
}