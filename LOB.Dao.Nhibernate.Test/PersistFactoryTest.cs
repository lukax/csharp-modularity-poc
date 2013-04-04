#region Usings

using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using LOB.Dao.Interface;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace LOB.Dao.Nhibernate.Test {
    [TestClass]
    public class PersistFactoryTest {

        [Import("Sql")]
        public IRepository Repository { get; set; }

        [TestMethod]
        public void GetInstanceTest() {
            //new PersistFactory(this);
            Assert.IsNotNull(Repository);
        }

        [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable"
            )]
        private class PersistFactory {

            private readonly AggregateCatalog _catalog;
            private readonly CompositionContainer _container;

            private readonly IUnityContainer ccontainer = new UnityContainer();
            [Import] private Inner inner;

            public PersistFactory(object obj) {
                Debug.WriteLine("Tryng to load dll from: " +
                                Assembly.GetExecutingAssembly().Location);

                _catalog = new AggregateCatalog(
                    new AssemblyCatalog(Assembly.GetExecutingAssembly()),
                    new AssemblyCatalog(Assembly.LoadFrom("LOB.Dao.Nhibernate.dll")));
                _container = new CompositionContainer(_catalog);
                //_container.SatisfyImportsOnce(this);
                //_container.SatisfyImportsOnce(obj);
                _container.ComposeParts(this, obj);

                Assert.AreEqual(ccontainer, inner.container);
            }

            /// <summary>
            ///     Compose a part, making the imports work
            /// </summary>
            /// <param name="obj">Object to compose</param>
            public void Compose(object obj) { _container.ComposeParts(obj); }

            public IRepository GetInstance(PersistType type = PersistType.MySql) {
                if(type == PersistType.MySql) return _container.GetExportedValue<IRepository>("Sql");

                if(type == PersistType.Memory) return _container.GetExportedValue<IRepository>("GetList");

                if(type == PersistType.File) return _container.GetExportedValue<IRepository>("File");

                throw new ArgumentNullException();
            }

            private class Inner {

                public readonly IUnityContainer container;

                [InjectionConstructor]
                public Inner(IUnityContainer container) { this.container = container; }

            }

        }

    }
}