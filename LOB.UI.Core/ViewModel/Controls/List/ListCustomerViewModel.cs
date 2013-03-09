#region Usings

using System;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.ViewModel.Controls.List;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List
{
    public sealed class ListCustomerViewModel : ListBaseEntityViewModel<Customer>, IListCustomerViewModel
    {
        public ListCustomerViewModel(Customer entity, IRepository repository)
            : base(entity, repository)
        {
            Entity = entity;
            if (Entity.Person == null)
                throw new ArgumentException("Entity has not defined a person");
        }

        public new Expression<Func<Employee, bool>> SearchCriteria
        {
            get
            {
                try
                {
                    return (arg =>
                            arg.Title.ToUpper().Contains(Search.ToUpper())
                            || arg.HireDate.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Code.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Title.ToUpper().Contains(Search.ToUpper())
                            || arg.FirstName.ToUpper().Contains(Search.ToUpper())
                            || arg.LastName.ToUpper().Contains(Search.ToUpper())
                            || arg.NickName.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Notes.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Rg.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Cpf.ToString().ToUpper().Contains(Search.ToUpper()));
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