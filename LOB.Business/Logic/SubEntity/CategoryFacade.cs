#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Contract.Logic.Base;
using LOB.Business.Contract.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Core.Localization;
using LOB.Dao.Contract;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    [Export(typeof(ICategoryFacade)), Export(typeof(IBaseEntityFacade<Category>)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class CategoryFacade : BaseEntityFacade<Category>, ICategoryFacade {
        [ImportingConstructor]
        public CategoryFacade(IRepository repository)
            : base(repository) { ConfigureValidations(); }

        public override Category GenerateEntity() {
            var result = base.GenerateEntity();
            result.Name = "";
            result.Description = "";
            return result;
        }

        public void ConfigureValidations() {
            AddValidation((sender, name) => string.IsNullOrWhiteSpace(Entity.Name) ? new ValidationResult("Name", Strings.Common_Name) : null);
            AddValidation(
                (sender, name) =>
                string.IsNullOrWhiteSpace(Entity.Description) ? new ValidationResult("Description", Strings.Common_Description) : null);
        }
    }
}