#region Usings

using System;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List
{
    public sealed class ListCustomerViewModel : ListPersonViewModel<Person>
    {
        public ListCustomerViewModel(Customer client, IRepository repository)
            : base(client.Person, repository)
        {
            Entity = client;
        }

        public new Customer Entity { get; set; }

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