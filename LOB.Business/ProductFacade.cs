#region Usings

using LOB.Business.Interface;
using LOB.Dao.Interface;
using LOB.Domain;

#endregion

namespace LOB.Business
{
    public class ProductFacade : EntityFacade<Product>, IProductFacade
    {
        public ProductFacade(IUnityOfWork unityOfWork, Product entity)
            : base(unityOfWork, entity)
        {
        }
    }
}