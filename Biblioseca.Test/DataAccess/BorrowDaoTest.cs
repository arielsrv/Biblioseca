using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Borrows;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NUnit.Framework;

namespace Biblioseca.Test.DataAccess
{
    [TestFixture]
    public class BorrowDaoTest
    {
        [SetUp]
        public void SetUp()
        {
            sessionFactory = new Configuration().Configure().BuildSessionFactory();
            session = sessionFactory.OpenSession();
            transaction = session.BeginTransaction();
            CurrentSessionContext.Bind(session);
        }

        [TearDown]
        public void CleanUp()
        {
            transaction.Rollback();
            session.Close();
        }

        private ISessionFactory sessionFactory;
        private ISession session;
        private ITransaction transaction;

        [Test]
        public void GetAll()
        {
            BorrowDao borrowDao = new BorrowDao(sessionFactory);

            IEnumerable<Borrow> borrows = borrowDao.GetAll();

            Assert.That(borrows.Any());
        }

        [Test]
        public void GetByBook()
        {
            BorrowDao borrowDao = new BorrowDao(sessionFactory);

            IEnumerable<Borrow> borrows = borrowDao.GetBorrowsByBookId(1);

            Assert.That(borrows.Any());
        }

        [Test]
        public void GetAllByBookAndPartner()
        {
            BorrowDao borrowDao = new BorrowDao(sessionFactory);

            IEnumerable<Borrow> borrows = borrowDao.GetBorrows(1, 1);

            Assert.That(borrows.Any());
        }

        [Test]
        public void GetByBookAndPartner()
        {
            BorrowDao borrowDao = new BorrowDao(sessionFactory);

            Borrow borrow = borrowDao.GetBorrow(199, 139);

            Assert.That(borrow != null);
        }
    }
}
