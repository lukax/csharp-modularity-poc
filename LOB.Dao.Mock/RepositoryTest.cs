#region Usings

using LOB.Dao.Interface;
using LOB.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

#endregion

namespace LOB.Dao.Mock {
    [TestClass]
    public class RepositoryTest {

        [TestMethod]
        public void MockstartTest() {
            //var session = new Mock<ISessionCreator>(MockBehavior.Strict);
            //var uow = new Mock<IUnityOfWork>(MockBehavior.Strict);
            //var repo = new Mock<IRepository>(MockBehavior.Strict);

            //var product = new Product {Description = "Teste description", UnitsInStock = 12};
            //var operations = new {Add = "1", Update = "2", Remove = "3"};

            //uow.Setup(x => x.ORM).Returns(() => operations);
            //repo.Setup(x => x.Save(product)).Returns(() => {
            //                                             uow.Object.Save(product);
            //                                             return product;
            //                                         });

            //repo.Setup(x=> x.)
        }

    }
}