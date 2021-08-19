using Biblioseca.DataAccess.Authors;
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
    public class AuthorDaoTest
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
            AuthorDao authorDao = new AuthorDao(this.sessionFactory);

            IEnumerable<Author> authors = authorDao.GetAll();

            Assert.IsTrue(authors.Any());
        }

        [Test]
        public void GetByHqlQuery()
        {
            AuthorDao authorDao = new AuthorDao(this.sessionFactory);

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "FirstName", "%Steve%" }
            };

            Author author = authorDao.GetUniqueByHqlQuery("FROM Author WHERE FirstName LIKE :FirstName", parameters);

            Assert.IsNotNull(author);
            Assert.AreEqual("Steve", author.FirstName);
        }

        [Test]
        public void GetByQuery()
        {
            AuthorDao authorDao = new AuthorDao(this.sessionFactory);

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "FirstName", "Steve" }
            };

            Author author = authorDao.GetUniqueByQuery(parameters);

            Assert.IsNotNull(author);
            Assert.AreEqual("Steve", author.FirstName);
        }
    }
}