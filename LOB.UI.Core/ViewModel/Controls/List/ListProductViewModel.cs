#region Usings

using System;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.ViewModel.Controls.List;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List
{
    public sealed class ListProductViewModel : ListBaseEntityViewModel<Product>, IListProductViewModel
    {
        [InjectionConstructor]
        public ListProductViewModel(Product product, IRepository repository)
            : base(product, repository)
        {
        }

        public new Expression<Func<Product, bool>> SearchCriteria
        {
            get
            {
                try
                {
                    return (arg =>
                            arg.Code.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Name.ToUpper().Contains(Search.ToUpper())
                            || arg.Description.ToUpper().Contains(Search.ToUpper())
                            || arg.UnitSalePrice.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.ProfitMargin.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Status.ToString().ToUpper().Contains(Search.ToUpper()));
                }
                catch (FormatException)
                {
                    return arg => false;
                }
            }
        }

        protected override bool CanUpdate(object arg)
        {
            //TODO: Business logic
            return true;
        }

        protected override bool CanDelete(object arg)
        {
            //TODO: Business logic
            return true;
        }
    }
}