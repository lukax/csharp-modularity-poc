using System.Collections.Generic;
using LOB.Business.Interface;
using LOB.Business.Interface.Logic.Base;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Core.Localization;
using LOB.Domain.SubEntity;

namespace LOB.Business.Logic.SubEntity
{
    public class CategoryFacade : ServiceFacade<Category>, ICategoryFacade
    {
        private Category _entity;
        public override Category Entity
        {
            get { return _entity; }
            set
            {
                _entity = value;
                base.AddValidators(_entity);
                this.AddValidadors(_entity);
            }
        }

        private void AddValidadors(Category c)
        {

        }

        public override bool CanAdd(out IEnumerable<InvalidField> invalidFields)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanUpdate(out IEnumerable<InvalidField> invalidFields)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanDelete(out IEnumerable<InvalidField> invalidFields)
        {
            throw new System.NotImplementedException();
        }

        public override void GenerateEntity()
        {
            Entity = new Category
                {
                    Description = "",
                    Name = "",
                };
        }
    }
}
