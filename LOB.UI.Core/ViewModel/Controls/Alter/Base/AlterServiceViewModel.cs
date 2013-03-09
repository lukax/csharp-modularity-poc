#region Usings

using System;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.Base
{
    public abstract class AlterServiceViewModel<T> : AlterBaseEntityViewModel<T>, IAlterServiceViewModel<T>
        where T : Service
    {
        public AlterServiceViewModel(T entity, IRepository repository)
            : base(entity,repository)
        {
        }

        protected override void QuickSearch(object arg)
        {
            throw new NotImplementedException();
        }

        protected override void ClearEntity(object arg)
        {
            throw new NotImplementedException();
        }
    }
}