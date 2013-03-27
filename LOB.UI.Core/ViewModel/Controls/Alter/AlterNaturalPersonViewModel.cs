#region Usings

using System;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    public class AlterNaturalPersonViewModel : AlterBaseEntityViewModel<NaturalPerson>, IAlterNaturalPersonViewModel {
        private IUnityContainer _container;

        [InjectionConstructor]
        public AlterNaturalPersonViewModel(NaturalPerson entity, IRepository repository)
            : base(entity, repository) {
        }


        public string BirthDate {
            get { return Entity.BirthDate.ToShortDateString(); }
            set {
                if (Entity.BirthDate.ToShortDateString() == value) return;

                DateTime parsed;
                if (DateTime.TryParse(value, out parsed)) {
                    Entity.BirthDate = parsed;
                }
            }
        }

        public override void InitializeServices() {
            throw new NotImplementedException();
        }

        public override void Refresh() {
            throw new NotImplementedException();
        }

        public override OperationType OperationType {
            get { return OperationType.AlterNaturalPerson; }
        }

        public IAlterAddressViewModel AlterAddressViewModel { get; set; }
        public IAlterContactInfoViewModel AlterContactInfoViewModel { get; set; }

        protected override void SaveChanges(object arg) {
            using (Repository.Uow) {
                Repository.Uow.BeginTransaction();
                Repository.SaveOrUpdate(Entity);
                Repository.Uow.CommitTransaction();
            }
        }

        protected override void QuickSearch(object arg) {
            //Messenger.Default.Send<object>(_container.Resolve<ListNaturalPersonViewModel>(),"QuickSearchCommand");
        }

        protected override void ClearEntity(object arg) {
            Entity = new NaturalPerson();
        }
    }
}