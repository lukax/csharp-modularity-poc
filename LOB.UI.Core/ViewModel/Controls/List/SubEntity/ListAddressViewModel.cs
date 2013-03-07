﻿#region Usings

using System;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity
{
    public class ListAddressViewModel : ListBaseEntityViewModel<Address>
    {
        public ListAddressViewModel(Address entity, IRepository repository) : base(entity, repository)
        {
        }

        public new Expression<Func<Address, bool>> SearchCriteria
        {
            get
            {
                try
                {
                    return (arg =>
                            arg.Code.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.City.ToUpper().Contains(Search.ToUpper())
                            || arg.Country.ToUpper().Contains(Search.ToUpper())
                            || arg.District.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Street.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.StreetComplement.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.StreetNumber.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.ZipCode.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.State.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Status.ToString().ToUpper().Contains(Search.ToUpper()));
                }
                catch (FormatException)
                {
                    return arg => false;
                }
            }
        }
    }
}