﻿#region Usings

using System;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity
{
    public class ListPayCheckViewModel : ListBaseEntityViewModel<PayCheck>
    {
        public ListPayCheckViewModel(PayCheck entity, IRepository repository) : base(entity, repository)
        {
        }

        public new Expression<Func<PayCheck, bool>> SearchCriteria
        {
            get
            {
                try
                {
                    return (arg =>
                            arg.Code.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Ps.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.Bonus.ToString().ToUpper().Contains(Search.ToUpper())
                            || arg.CurrentSalary.ToString().ToUpper().Contains(Search.ToUpper()));
                }
                catch (FormatException)
                {
                    return arg => false;
                }
            }
        }
    }
}