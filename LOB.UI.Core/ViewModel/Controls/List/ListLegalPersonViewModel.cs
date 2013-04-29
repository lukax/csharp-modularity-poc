#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using LOB.Domain;
using LOB.UI.Contract.ViewModel.Controls.List;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List {
    [Export(typeof(IListLegalPersonViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class ListLegalPersonViewModel : ListBaseEntityViewModel<LegalPerson>, IListLegalPersonViewModel {
        public override Expression<Func<LegalPerson, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) || arg.TradingName.ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.CorporateName.ToUpper().Contains(SearchString.ToUpper()) || arg.CNPJ.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.InscEstadual.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.InscMunicipal.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.Notes.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.CorporateName.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }
    }
}