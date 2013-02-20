#region Usings

using LOB.Domain;
using LOB.Domain.SubEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

#endregion

namespace LOB.Dao.Nhibernate.Test
{
    [TestClass]
    public class SessionCreatorTest
    {
        [TestMethod]
        public void CreateSessionTest()
        {
            var creator = new SessionCreator();
            Assert.IsNotNull(creator.Orm);
        }

        [TestMethod]
        public void TransactionTest()
        {
            var sCreator = new SessionCreator();

            var address = new Address
                {
                    Country = "Brazil",
                    State = "RJ",
                    Status = AdressStatus.Active,
                    Street = "Street b",
                    StreetNumber = 1001,
                    ZipCode = 123456789
                };
            Email e1 = "thisdude@you.com";
            Email e2 = "another@you.com";
            var contact = new ContactInfo()
                {
                    PhoneNumbers = new PhoneNumber[]
                        {
                            new PhoneNumber
                                {
                                    Number = 123456,
                                    Description = "Casa",
                                    NumberType = NumberType.Telephone
                                },
                            new PhoneNumber
                                {
                                    Number = 1234567,
                                    Description = "Trabalho",
                                    NumberType = NumberType.Cellphone
                                }
                        },
                    WebSite = "www.thisdude.com",
                    Ps = "Hes at home by the night",
                    Status = ContactStatus.Active,
                    //Contact = "Hes at home by the night",
                    Emails = new[] {e1, e2}
                };


            var stores = new Store[]
                {
                    new Store {Name = "Fresh Bar"},
                    new Store {Name = "Total Drinks"}
                };
            var entity = new Company()
                {
                    TradingName = "Coke",
                    CorporateName = "The Coca-Cola Company",
                    Address = address,
                    ContactInfo = contact,
                    // Stores = stores,
                };


            var session = ((ISession) sCreator.Orm);
            using (session)
            {
                session.BeginTransaction();
                session.Save(entity);
                Assert.IsTrue((session).Contains(entity));
                session.Transaction.Commit();
            }
        }
    }
}