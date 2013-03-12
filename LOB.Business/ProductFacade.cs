#region Usings

using LOB.Business.Interface;
using LOB.Domain;

#endregion

namespace LOB.Business
{
    public class ProductFacade : EntityFacade<Product>, IProductFacade
    {
        public ProductFacade(Product entity)
            : base(entity)
        {
        }
    }
}