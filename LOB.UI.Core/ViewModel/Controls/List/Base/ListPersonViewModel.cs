#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using LOB.Dao.Interface;
using LOB.Domain.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.Base
{
    public class ListPersonViewModel : ListBaseEntityViewModel<Person>
    {
        #region Props

        #endregion

        public ListPersonViewModel(Person entity, IRepository repository)
            : base(entity, repository)
        {
        }

        public override bool CanUpdate(object arg)
        {
            //TODO: Business logic
            return true;
        }

        public override bool CanDelete(object arg)
        {
            //TODO: Business logic
            return true;
        }
    }
}