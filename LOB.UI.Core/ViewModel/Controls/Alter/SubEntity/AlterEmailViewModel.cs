#region Usings

using System;
using System.Collections.Generic;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Dao.Interface;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity
{
    public sealed class AlterEmailViewModel : AlterBaseEntityViewModel<Email>, IAlterEmailViewModel
    {
        private readonly IEmailFacade _emailFacade;

        public AlterEmailViewModel(Email entity, IRepository repository, IEmailFacade emailFacade)
            : base(entity, repository)
        {
            _emailFacade = emailFacade;
        }

        public override void InitializeServices()
        {
            Refresh();
        }

        public override void Refresh()
        {
            Entity = new Email
                {
                    Value = "",
                };
            _emailFacade.SetEntity(Entity);
            _emailFacade.ConfigureValidations();
        }

        protected override bool CanSaveChanges(object arg)
        {
            IEnumerable<ValidationResult> results;
            return _emailFacade.CanAdd(out results);
        }

        public override OperationType OperationType
        {
            get { return OperationType.AlterEmail; }
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