using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LOB.UI.Core.ViewModel.Controls.List
{
    public class ListNaturalPersonViewModel : ListPersonViewModel<NaturalPerson>
    {
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
    }
}
