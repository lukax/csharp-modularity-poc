#region Usings

using System;
using System.ComponentModel;
using System.Diagnostics;

//using NullGuard;

#endregion

namespace LOB.Domain.Base {
    [Serializable]
    public abstract class BaseEntity : BaseNotifyChange, IDataErrorInfo, IEquatable<BaseEntity> {
        protected BaseEntity() { Id = default(Guid); }
        public Guid Id { get; private set; }
        public long Code { get; set; }

        public string this[string columnName] {
            get { return ValidationFunc(columnName); }
        }
        /// <summary>
        ///     Function which gets executed when Indexer is called.
        ///     In: columnName, Out: error message
        /// </summary>
        public Func<string, string> ValidationFunc { get; set; }
        public string Error {
            get { return this[null]; }
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