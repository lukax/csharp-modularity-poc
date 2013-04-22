#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.ViewModel.Controls.List.SubEntity;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity {
    [Export(typeof(IListContactInfoViewModel))]
    public class ListContactInfoViewModel : ListBaseEntityViewModel<ContactInfo>, IListContactInfoViewModel {
        [ImportingConstructor]
        public ListContactInfoViewModel(IRepository repository, IEventAggregator eventAggregator)
            : base() { }

        public override void InitializeServices() { base.InitializeServices(); }

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
    }
}