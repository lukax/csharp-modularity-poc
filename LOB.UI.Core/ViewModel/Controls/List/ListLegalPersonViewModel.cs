#region Usings

using System;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List {
    public class ListLegalPersonViewModel : ListBaseEntityViewModel<LegalPerson>, IListLegalPersonViewModel {
        public ListLegalPersonViewModel(LegalPerson entity, IRepository repository)
            : base(entity, repository) {
        }

        public new Expression<Func<LegalPerson, bool>> SearchCriteria {
            get {
                try {
                    return (arg =>
                            arg.Code.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.TradingName.ToUpper().Contains(Search.ToUpper())
                            || arg.CorporateName.ToUpper().Contains(Search.ToUpper())
                            || arg.Cnpj.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Iestadual.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Imunicipal.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Notes.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.CorporateName.ToString().ToUpper().Contains(Search.ToUpper()));
                }
                catch (FormatException) {
                    return arg => false;
                }
            }
        }

        public override OperationType OperationType {
            get { return OperationType.ListLegalPerson; }
        }
    }
}