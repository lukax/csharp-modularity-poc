#region Usings

using System.Collections.Generic;
using LOB.Business.Interface.Logic.Base;
using LOB.Domain.Base;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Logic.Base
{
    public class PersonFacade : IPersonFacade
    {
        //    private readonly IAddressFacade _addressFacade;
        //    private readonly IContactInfoFacade _contactInfoFacade;
        //    private TEntity _entity;

        //    public string Error { get; private set; }
        //    public TEntity Entity
        //    {
        //        get { return _entity; }
        //        set
        //        {
        //            _entity = value;
        //            this.AddValidators(_entity);
        //        }
        //    }

        //    public PersonFacade(IAddressFacade addressFacade, IContactInfoFacade contactInfoFacade)
        //    {
        //        _addressFacade = addressFacade;
        //        _contactInfoFacade = contactInfoFacade;
        //    }

        //    protected void AddValidators(Person entity)
        //    {
        //        entity.AddValidation((sender, name) => entity.Notes.Length > 300 ? new ValidationResult("Notes", Strings.Error_Field_TooLong) : null);
        //    }

        //    public bool CanAdd(out IEnumerable<InvalidField> invalidFields)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public bool CanUpdate(out IEnumerable<InvalidField> invalidFields)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public bool CanDelete(out IEnumerable<InvalidField> invalidFields)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public string this[string columnName]
        //    {
        //        get { throw new NotImplementedException(); }
        //    }

        void IPersonFacade.SetEntity<T>(T entity)
        {
            throw new System.NotImplementedException();
        }

        void IBaseEntityFacade.SetEntity<T>(T entity) 
        {
            throw new System.NotImplementedException();
        }

        public void ConfigureValidations()
        {
            throw new System.NotImplementedException();
        }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields)
        {
            throw new System.NotImplementedException();
        }

        public bool CanUpdate(out IEnumerable<ValidationResult> invalidFields)
        {
            throw new System.NotImplementedException();
        }

        public bool CanDelete(out IEnumerable<ValidationResult> invalidFields)
        {
            throw new System.NotImplementedException();
        }
    }
}