#region Usings

using System;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List.SubEntity;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity {
    public class ListContactInfoViewModel : ListBaseEntityViewModel<ContactInfo>, IListContactInfoViewModel {

        public ListContactInfoViewModel(ContactInfo entity, IRepository repository, IEventAggregator eventAggregator)
            : base(entity, repository, eventAggregator) { }

        public new Expression<Func<ContactInfo, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.PS.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.SpeakWith.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.WebSite.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Status.ToString().ToUpper().Contains(Search.ToUpper()) || arg.Emails.ToString().ToUpper().Contains(Search.ToUpper()) ||
                         arg.PhoneNumbers.ToString().ToUpper().Contains(Search.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }

        public override void InitializeServices() { if (Equals(Operation, default(ViewID))) Operation = _operation; }

        public override void Refresh() { Search = ""; }

        private readonly ViewID _operation = new ViewID {Type = ViewType.ContactInfo, State = ViewState.List};

    }
}