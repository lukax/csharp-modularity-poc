﻿#region Usings

using System;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity
{
    public class ListPhoneNumberViewModel : ListBaseEntityViewModel<PhoneNumber>
    {
        public ListPhoneNumberViewModel(PhoneNumber entity, IRepository repository)
            : base(entity, repository)
        {
        }

        public new Expression<Func<PhoneNumber, bool>> SearchCriteria
        {
            get
            {
                try
                {
                    return (arg =>
                            arg.Code.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Number.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.PhoneNumberType.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Description.ToString().ToUpper().Contains(Search.ToUpper()));
                }
                catch (FormatException)
                {
                    return arg => false;
                }
            }
        }
    }
}