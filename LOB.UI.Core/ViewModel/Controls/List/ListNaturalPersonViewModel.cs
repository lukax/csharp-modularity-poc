#region Usings

using System;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List
{
    public class ListNaturalPersonViewModel : ListPersonViewModel<NaturalPerson>, IListNaturalPersonViewModel
    {
        [InjectionConstructor]
        public ListNaturalPersonViewModel(NaturalPerson entity, IRepository repository)
            : base(entity, repository)
        {
        }

        public new Expression<Func<NaturalPerson, bool>> SearchCriteria
        {
            get
            {
                try
                {
                    return (arg =>
                            arg.Code.ToString().ToUpper().Contains(Search.ToUpper())
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

        public override OperationType OperationType
        {
            get { return OperationType.ListNaturalPerson; }
        }
    }
}