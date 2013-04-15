#region Usings

using System;
using System.Diagnostics;
using System.Windows.Input;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain {
    [Serializable]
    public class Command : BaseEntity, IEquatable<Command> {
        public string Name { get; set; }
        public object Parameter { get; set; }
        public ICommand Task { get; set; }
        #region Implementation of IEquatable<Command>

        public bool Equals(Command other) {
            try {
                return base.Equals(other) && other.Name.Equals(Name) && other.Parameter.Equals(Parameter) && other.Task.Equals(Task);
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