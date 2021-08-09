using Biblioseca.DataAccess.Borrows;
using Biblioseca.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using System.Collections.Generic;
using System.Linq;

namespace Biblioseca.Test.DataAccess
{
    [TestClass]
    public class BorrowDaoTest
    {
        private ISessionFactory sessionFactory;
        private ISession session;
        private ITransaction transaction;

        [TestInitialize]
        public void SetUp()
        {
            this.sessionFactory = new Configuration().Configure().BuildSessionFactory();
            this.session = this.sessionFactory.OpenSession();
            this.transaction = this.session.BeginTransaction();
            CurrentSessionContext.Bind(this.session);
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.transaction.Rollback();
            this.session.Close();
        }

        [TestMethod]
        public void GetAll()
        {
            BorrowDao borrowDao = new BorrowDao(this.sessionFactory);

            IEnumerable<Borrow> borrows = borrowDao.GetAll();

            Assert.IsTrue(borrows.Any());
        }

        [TestMethod]
        public void GetByBook()
        {
            BorrowDao borrowDao = new BorrowDao(this.sessionFactory);

            IEnumerable<Borrow> borrows = borrowDao.GetBorrowsByBookId(1);

            Assert.IsTrue(borrows.Any());
        }

        [TestMethod]
        public void GetAllByBookAndPartner()
        {
            BorrowDao borrowDao = new BorrowDao(this.sessionFactory);

            IEnumerable<Borrow> borrows = borrowDao.GetBorrows(1, 1);

            Assert.IsTrue(borrows.Any());
        }

        [TestMethod]
        public void GetByBookAndPartner()
        {
            BorrowDao borrowDao = new BorrowDao(this.sessionFactory);

            Borrow borrow = borrowDao.GetBorrow(199, 139);

            Assert.IsNotNull(borrow);
        }
    }
}