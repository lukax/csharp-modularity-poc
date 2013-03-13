#region Usings

using System;

#endregion

namespace LOB.UI.Interface.ViewModel.Base
{
    public interface IBaseViewModel
    {
        String Header { get; set; }
        void InitializeServices();
        void Refresh();
    }
}