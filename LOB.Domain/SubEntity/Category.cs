#region Usings

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using LOB.Core.Localization;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity {
    [Serializable]
    public class Category : BaseEntity, IEquatable<Category> {
        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        public string Detail { get; set; }
        #region Implementation of IEquatable

        public bool Equals(Category other) {
            try {
                return base.Equals(other);
            } catch(NullReferenceException ex) {
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return false;
            }
        }

        #endregion
        public override string ToString() { return Name; }
    }
}