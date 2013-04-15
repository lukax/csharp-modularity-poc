#region Usings

using System;
using System.Diagnostics;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.Logic {
    [Serializable]
    public sealed class ValidationResult : BaseNotifyChange, IEquatable<ValidationResult> {
        public ValidationResult(string fieldName, string errorDescription) {
            FieldName = fieldName;
            ErrorDescription = errorDescription;
        }

        public string FieldName { get; set; }
        public string ErrorDescription { get; set; }
        #region Implementation of IEquatable<ValidationResult>

        public bool Equals(ValidationResult other) {
            try {
                return FieldName == other.FieldName && ErrorDescription == other.ErrorDescription;
            } catch(NullReferenceException ex) {
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return false;
            }
        }

        #endregion
    }

    public static class ValidationResultExtensions {
        public static ValidationResult FieldName(this ValidationResult validationResult, string fieldName) {
            validationResult.FieldName = fieldName;
            return validationResult;
        }

        public static ValidationResult ErrorDescription(this ValidationResult validationResult, string errorDescription) {
            validationResult.ErrorDescription = errorDescription;
            return validationResult;
        }
    }
}