#region Usings

using LOB.Domain;

#endregion

namespace LOB.Business
{
    public class ProductFacade : EntityFacade<Product>
    {
        public ProductFacade(Product product)
            : base(product) {
        }
    }
}