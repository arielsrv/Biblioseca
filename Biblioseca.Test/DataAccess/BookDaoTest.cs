using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Books.Filters;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NUnit.Framework;

namespace Biblioseca.Test.DataAccess
{
    [TestFixture]
    public class BookDaoTest
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
            BookDao bookDao = new BookDao(sessionFactory);

            IEnumerable<Book> books = bookDao.GetAll();

            Assert.That(books.Any());
        }

        [Test]
        public void GetByFilter()
        {
            BookDao bookDao = new BookDao(sessionFactory);

            BookFilterDto bookFilterDto = new BookFilterDto
            {
                Title = "Avengers",
                AuthorFirstName = "Steve"
            };

            IEnumerable<Book> books = bookDao.GetByFilter(bookFilterDto);

            Assert.That(books.Any());
        }
    }
}
