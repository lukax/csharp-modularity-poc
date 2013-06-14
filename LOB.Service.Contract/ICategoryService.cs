using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOB.Domain.SubEntity;

namespace LOB.Service.Contract
{
    public interface ICategoryService {
        Category One();
        IEnumerable<Category> All();
        

    }
}
