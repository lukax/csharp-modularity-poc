#region Usings
using System;

#endregion

namespace LOB.Domain.Logic {
    [Serializable] public class ValidationResult {

        public ValidationResult(string fieldName, string errorDescription) {
            this.FieldName = fieldName;
            this.ErrorDescription = errorDescription;
        }

        public string FieldName { get; private set; }
        public string ErrorDescription { get; private set; }

    }
}