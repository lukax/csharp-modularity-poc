using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOB.Business.Contract.Logic.SubEntity;
using LOB.Domain.SubEntity;
using LOB.Service.Contract;
using LOB.Service.Contract.DCO;

namespace LOB.Service
{
    public class CategoryService : ICategoryService {
        private readonly ICategoryFacade _categoryFacade;

        [ImportingConstructor]
        public CategoryService(ICategoryFacade categoryFacade) {
            _categoryFacade = categoryFacade;
        }

        public Contract.DCO.Category One(long code) { }
        public IEnumerable<Contract.DCO.Category> All() { throw new NotImplementedException(); }

    }
}
