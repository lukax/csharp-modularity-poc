#region Usings

using System;
using System.Diagnostics;

//using NullGuard;

#endregion

namespace LOB.Domain.Base {
    [Serializable]
    public abstract class BaseEntity : BaseNotifyChange, IEquatable<BaseEntity> {
        public Guid Id { get; protected set; }
        public long Code { get; protected set; }
        #region Implementation of IEquatable<BaseEntity>

        public bool Equals(BaseEntity other) {
            try {
                return
                        other.Code.Equals(Code) &&
                        other.Id.Equals(Id);
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