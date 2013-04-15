#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using LOB.Domain.Logic;

//using NullGuard;

#endregion

namespace LOB.Domain.Base {
    [Serializable]
    public abstract class BaseEntity : BaseNotifyChange, IDataErrorInfo, IEquatable<BaseEntity> {
        private readonly IList<ValidationDelegate> _validationFuncs = new List<ValidationDelegate>();
// ReSharper disable UnusedAutoPropertyAccessor.Local, NHibernate Set ID
        public Guid Id { get; private set; }
// ReSharper restore UnusedAutoPropertyAccessor.Local
        public int Code { get; set; }

        //[AllowNull]
        public virtual string this[string columnName] {
            get { return GetVal(columnName); }
        }

        private string GetVal(string columnName) {
            if(string.IsNullOrWhiteSpace(columnName)) return null;
            ValidationResult validation = GetValidations(columnName).FirstOrDefault(x => x.FieldName == columnName);
            //Task.Run(() => { validation = GetValidations(columnName).FirstOrDefault(x => x.FieldName == columnName); });
            return validation != null ? validation.ErrorDescription : null;
        }

        //[AllowNull]
        public virtual string Error { get; set; }
        public void AddValidation(ValidationDelegate func) { _validationFuncs.Add(func); }
        public void RemoveValidation(ValidationDelegate func) { if(ContainsValidation(func)) _validationFuncs.Remove(func); }
        public bool ContainsValidation(ValidationDelegate func) { return _validationFuncs.Contains(func); }
        public IEnumerable<ValidationResult> GetValidations(string propertyName) {
            var propName = propertyName;
            return
                _validationFuncs.Select(validationDel => validationDel(this, propertyName))
                                .Where(result => result != null)
                                .Where(result => result.FieldName == propName);
        }
        #region Implementation of IEquatable<BaseEntity>

        public bool Equals(BaseEntity other) {
            try {
                return other.Code.Equals(Code) && other.Id.Equals(Id);
            } catch(NullReferenceException ex) {
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return false;
            }
        }

        #endregion
    }
}