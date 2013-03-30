#region Usings
using System;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.Base {
    public class AlterServiceViewModel : AlterBaseEntityViewModel<Service>, IAlterServiceViewModel {

        public AlterServiceViewModel(Service entity, IRepository repository)
            : base(entity, repository) {}

        public Service Entity { get; set; }

        public override void InitializeServices() {
            throw new NotImplementedException();
        }

        public override void Refresh() {
            throw new NotImplementedException();
        }

        public override OperationType OperationType {
            get { throw new NotImplementedException(); }
        }

        protected override void QuickSearch(object arg) {}

        protected override void ClearEntity(object arg) {}

    }
}