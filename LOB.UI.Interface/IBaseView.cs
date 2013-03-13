#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.UI.Interface.ViewModel.Base;

#endregion

namespace LOB.UI.Interface
{
    [InheritedExport]
    public interface IBaseView 
    {
        IBaseViewModel ViewModel { get; set; }
        String Header { get; set; }
        int? Index { get; set; }
        void InitializeServices();
        void Refresh();
    }
}