#region Usings

using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;

#endregion

namespace LOB.Core
{
    public interface IComposableComponent
    {
        CompositionContainer ComponentContainer { get; }
        ComposablePartCatalog ComponentCatalog { get; }
        void Compose(params object[] objects);
        void Dispose();
    }
}