using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Authors;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NUnit.Framework;

namespace Biblioseca.Test.DataAccess
{
    [TestFixture]
    public class AuthorDaoTest
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
            AuthorDao authorDao = new AuthorDao(sessionFactory);

            IEnumerable<Author> authors = authorDao.GetAll();

            Assert.That(authors.Any());
        }

        [Test]
        public void GetByHqlQuery()
        {
            AuthorDao authorDao = new AuthorDao(sessionFactory);

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "FirstName", "%Steve%" }
            };

            Author author = authorDao.GetUniqueByHqlQuery("FROM Author WHERE FirstName LIKE :FirstName", parameters);

            Assert.That(author != null);
            Assert.Equals("Steve", author.FirstName);
        }

        [Test]
        public void GetByQuery()
        {
            AuthorDao authorDao = new AuthorDao(sessionFactory);

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "FirstName", "Steve" }
            };

            Author author = authorDao.GetUniqueByQuery(parameters);

            Assert.That(author != null);
            Assert.Equals("Steve", author.FirstName);
        }
    }
}
