#region Usings

using System;
using LOB.UI.Interface.ViewModel.Base;

#endregion

namespace LOB.UI.Interface
{
    public interface IBaseView
    {
        IBaseViewModel ViewModel { get; set; }
        String Header { get; set; }
        int? Index { get; set; }
        void InitializeServices();
        void Refresh();
    }
}