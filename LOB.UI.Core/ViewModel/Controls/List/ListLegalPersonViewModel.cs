#region Usings
using System;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List {
    public class ListLegalPersonViewModel : ListBaseEntityViewModel<LegalPerson>, IListLegalPersonViewModel {

        [InjectionConstructor] public ListLegalPersonViewModel(LegalPerson entity, IRepository repository,
            EventAggregator eventAggregator)
            : base(entity, repository, eventAggregator) {}

        public new Expression<Func<LegalPerson, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString().ToUpper().Contains(Search.ToUpper()) ||
                         arg.TradingName.ToUpper().Contains(Search.ToUpper()) ||
                         arg.CorporateName.ToUpper().Contains(Search.ToUpper()) ||
                         arg.Cnpj.ToString().ToUpper().Contains(Search.ToUpper()) ||
                         arg.Iestadual.ToString().ToUpper().Contains(Search.ToUpper()) ||
                         arg.Imunicipal.ToString().ToUpper().Contains(Search.ToUpper()) ||
                         arg.Notes.ToString().ToUpper().Contains(Search.ToUpper()) ||
                         arg.CorporateName.ToString().ToUpper().Contains(Search.ToUpper()));
                }
                catch(FormatException) {
                    return arg => false;
                }
            }
        }

        public override void Refresh() {
            throw new NotImplementedException();
        }

        public override OperationType OperationType {
            get { return OperationType.ListLegalPerson; }
        }

    }
}