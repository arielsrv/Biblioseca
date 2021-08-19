using Biblioseca.DataAccess.Borrows;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Biblioseca.Test.DataAccess
{
    [TestFixture]
    public class BorrowDaoTest
    {
        private ISessionFactory sessionFactory;
        private ISession session;
        private ITransaction transaction;

        [SetUp]
        public void SetUp()
        {
            this.sessionFactory = new Configuration().Configure().BuildSessionFactory();
            this.session = this.sessionFactory.OpenSession();
            this.transaction = this.session.BeginTransaction();
            CurrentSessionContext.Bind(this.session);
        }

        [TearDown]
        public void CleanUp()
        {
            this.transaction.Rollback();
            this.session.Close();
        }

        [Test]
        public void GetAll()
        {
            BorrowDao borrowDao = new BorrowDao(this.sessionFactory);

            IEnumerable<Borrow> borrows = borrowDao.GetAll();

            Assert.IsTrue(borrows.Any());
        }

        [Test]
        public void GetByBook()
        {
            BorrowDao borrowDao = new BorrowDao(this.sessionFactory);

            IEnumerable<Borrow> borrows = borrowDao.GetBorrowsByBookId(1);

            Assert.IsTrue(borrows.Any());
        }

        [Test]
        public void GetAllByBookAndPartner()
        {
            BorrowDao borrowDao = new BorrowDao(this.sessionFactory);

            IEnumerable<Borrow> borrows = borrowDao.GetBorrows(1, 1);

            Assert.IsTrue(borrows.Any());
        }

        [Test]
        public void GetByBookAndPartner()
        {
            BorrowDao borrowDao = new BorrowDao(this.sessionFactory);

            Borrow borrow = borrowDao.GetBorrow(199, 139);

            Assert.IsNotNull(borrow);
        }
    }
}