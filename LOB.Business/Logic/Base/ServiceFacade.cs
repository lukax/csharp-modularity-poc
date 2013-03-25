#region Usings



#endregion

namespace LOB.Business.Logic.Base
{
    //public abstract class ServiceFacade<TEntity> : IServiceFacade<TEntity> where TEntity : Service
    //{
    ////    private TEntity _entity;
    ////    public virtual TEntity Entity
    ////    {
    ////        get { return _entity; }
    ////        set
    ////        {
    ////            if (_entity == value) return;  
    ////            _entity = value;
    ////            AddValidators(_entity);        
    ////        }
    ////    }

    ////    protected void AddValidators(Service s)
    ////    {
    ////        s.AddValidation((sender, name) => s.Name.Length < 1 ? new ValidationResult("Name", Strings.Error_Field_Empty) : null); 
    ////        s.AddValidation((sender, name) => s.Description.Length > 300 ? new ValidationResult("Description", Strings.Error_Field_TooLong) : null);
    ////    }

    ////    public virtual bool CanAdd(out IEnumerable<InvalidField> invalidFields)
    ////    {
    ////        throw new NotImplementedException();
    ////    }

    ////    public virtual bool CanUpdate(out IEnumerable<InvalidField> invalidFields)
    ////    {
    ////        throw new NotImplementedException();
    ////    }

    ////    public virtual bool CanDelete(out IEnumerable<InvalidField> invalidFields)
    ////    {
    ////        throw new NotImplementedException();
    ////    }


    ////    public abstract void GenerateEntity();
    //}
}