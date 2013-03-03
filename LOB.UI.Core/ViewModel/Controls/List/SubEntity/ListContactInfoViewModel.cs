#region Usings

using System;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity
{
    public class ListContactInfoViewModel : ListBaseEntityViewModel<ContactInfo>
    {
        public ListContactInfoViewModel(ContactInfo entity, IRepository repository) : base(entity, repository)
        {
        }

        public new Expression<Func<ContactInfo, bool>> SearchCriteria
        {
            get
            {
                try
                {
                    return (arg =>
                            arg.Code.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Ps.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.SpeakWith.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.WebSite.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Status.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Emails.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.PhoneNumbers.ToString().ToUpper().Contains(Search.ToUpper()));
                }
                catch (FormatException)
                {
                    return arg => false;
                }
            }
        }
    }
}