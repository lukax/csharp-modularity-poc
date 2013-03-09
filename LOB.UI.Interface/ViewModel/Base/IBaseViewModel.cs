#region Usings

using System;
using System.ComponentModel.Composition;

#endregion

namespace LOB.UI.Interface.ViewModel.Base
{
    [InheritedExport]
    public interface IBaseViewModel
    {
        String Header { get; set; }
        void InitializeServices();
        void Refresh();
    }
}