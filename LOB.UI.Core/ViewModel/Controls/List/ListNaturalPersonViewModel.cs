#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.ViewModel.Controls.List;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List {
    [Export(typeof(IListNaturalPersonViewModel))]
    public class ListNaturalPersonViewModel : ListBaseEntityViewModel<NaturalPerson>, IListNaturalPersonViewModel {
        [ImportingConstructor]
        public ListNaturalPersonViewModel(IRepository repository, IEventAggregator eventAggregator)
            : base() { }

        public new Expression<Func<NaturalPerson, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Culture).ToUpper().Contains(Search.ToUpper()) || arg.FirstName.ToUpper().Contains(Search.ToUpper()) ||
                         arg.LastName.ToUpper().Contains(Search.ToUpper()) || arg.NickName.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Notes.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.RG.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.CPF.ToString(Culture).ToUpper().Contains(Search.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }
    }
}