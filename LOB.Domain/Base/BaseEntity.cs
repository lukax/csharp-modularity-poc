#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using LOB.Domain.Logic;
using NullGuard;

#endregion

namespace LOB.Domain.Base {
    [Serializable]
    public abstract class BaseEntity : BaseNotifyChange, IDataErrorInfo {

        private readonly IList<ValidationDelegate> _validationFuncs = new List<ValidationDelegate>();
// ReSharper disable UnusedAutoPropertyAccessor.Local, NHibernate Set ID
        public Guid Id { get; private set; }
// ReSharper restore UnusedAutoPropertyAccessor.Local
        public int Code { get; set; }

        [AllowNull]
        public virtual string this[string columnName] {
            get {
                var firstOrDefault =
                    GetValidations(columnName).FirstOrDefault(x => x.FieldName == columnName);
                return firstOrDefault != null ? firstOrDefault.ErrorDescription : null;
            }
        }

        [AllowNull]
        public virtual string Error { get; set; }

        public void AddValidation(ValidationDelegate func) { _validationFuncs.Add(func); }

        public void RemoveValidation(ValidationDelegate func) { if(_validationFuncs.Contains(func)) _validationFuncs.Remove(func); }

        public IList<ValidationResult> GetValidations(string propertyName) {
            return
                _validationFuncs.Select(validationDel => validationDel(this, propertyName))
                                .Where(result => result != null)
                                .Where(result => result.FieldName == propertyName)
                                .ToList();
        }

    }
}