#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using LOB.Domain.SubEntity;
using LOB.UI.Contract.ViewModel.Controls.List.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity {
    [Export(typeof(IListContactInfoViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class ListContactInfoViewModel : ListBaseEntityViewModel<ContactInfo>, IListContactInfoViewModel {
        public override Expression<Func<ContactInfo, bool>> SearchCriteria {
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