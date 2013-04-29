#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using LOB.Domain;
using LOB.UI.Contract.ViewModel.Controls.List;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List {
    [Export(typeof(IListNaturalPersonViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class ListNaturalPersonViewModel : ListBaseEntityViewModel<NaturalPerson>, IListNaturalPersonViewModel {
        public override Expression<Func<NaturalPerson, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) || arg.FirstName.ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.LastName.ToUpper().Contains(SearchString.ToUpper()) || arg.NickName.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.Notes.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.RG.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.CPF.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }
    }
}