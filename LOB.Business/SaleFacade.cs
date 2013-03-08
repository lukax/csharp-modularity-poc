#region Usings

using LOB.Domain;

#endregion

namespace LOB.Business
{
    public class SaleFacade : EntityFacade<Sale>
    {
        public SaleFacade(Sale entity)
            : base(entity)
        {
        }
    }
}