#region Usings

using System;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.Logic {
    [Serializable]
    public class ValidationResult : BaseNotifyChange {

        public ValidationResult(string fieldName, string errorDescription) {
            FieldName = fieldName;
            ErrorDescription = errorDescription;
        }

        public string FieldName { get; private set; }
        public string ErrorDescription { get; private set; }

    }
}