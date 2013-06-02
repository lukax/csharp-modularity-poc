#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Contract.Logic.Base;
using LOB.Business.Contract.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Dao.Contract;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    [Export(typeof(ICategoryFacade)), Export(typeof(IBaseEntityFacade<Category>)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class CategoryFacade : BaseEntityFacade, ICategoryFacade {
        [ImportingConstructor]
        public CategoryFacade(IRepository repository)
                : base(repository) { }

        public Category Generate() {
            var result = new Category {Name = "", Description = ""};
            return result;
        }
    }
}