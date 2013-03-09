#region Usings

using LOB.Domain.Base;

#endregion

namespace LOB.UI.Interface.ViewModel.Controls.Alter.Base
{
    public interface IAlterServiceViewModel<T> : IAlterBaseEntityViewModel<T> where T : Service
    {
    }
}