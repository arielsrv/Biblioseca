using Biblioseca.DataAccess.Authors;
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
    public class AuthorDaoTest
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
            AuthorDao authorDao = new AuthorDao(this.sessionFactory);

            IEnumerable<Author> authors = authorDao.GetAll();

            Assert.IsTrue(authors.Any());
        }

        [TestMethod]
        public void GetByHqlQuery()
        {
            AuthorDao authorDao = new AuthorDao(this.sessionFactory);

            IDictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("FirstName", "%Steve%");

            Author author = authorDao.GetUniqueByHqlQuery("FROM Author WHERE FirstName LIKE :FirstName", parameters);

            Assert.IsNotNull(author);
            Assert.AreEqual("Steve", author.FirstName);
        }

        [TestMethod]
        public void GetByQuery()
        {
            AuthorDao authorDao = new AuthorDao(this.sessionFactory);

            IDictionary<string, object> parameters = new Dictionary<string, object> { { "FirstName", "Steve" } };
            Author author = authorDao.GetUniqueByQuery(parameters);

            Assert.IsNotNull(author);
            Assert.AreEqual("Steve", author.FirstName);
        }
    }
}