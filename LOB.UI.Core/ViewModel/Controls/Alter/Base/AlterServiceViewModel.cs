using System;
using LOB.Dao.Interface;
using LOB.Domain.Base;

namespace LOB.UI.Core.ViewModel.Controls.Alter.Base
{
    public class AlterServiceViewModel : AlterBaseEntityViewModel<Service>
    {
        public AlterServiceViewModel(Service entity, IRepository repository)
            : base(entity, repository)
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
