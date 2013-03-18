#region Usings

using System;

#endregion

namespace LOB.UI.Interface.ViewModel.Base
{
    public interface IBaseViewModel
    {
        string Header { get; set; }
        void InitializeServices();
        void Refresh();
    }
}