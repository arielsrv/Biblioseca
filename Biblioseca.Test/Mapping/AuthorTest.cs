using Biblioseca.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Linq;

namespace Biblioseca.Test.Mapping
{
    [TestClass]
    public class AuthorTests
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
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.transaction.Rollback();
            this.session.Close();
        }

        [TestMethod]
        public void CreateAuthor()
        {
            Author author = new Author
            {
                FirstName = "Wanda",
                LastName = "Maximoff"
            };

            this.session.Save(author);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(author.Id > 0);

            Author created = this.session.Get<Author>(author.Id);

            Assert.AreEqual(author.Id, created.Id);
        }

        [TestMethod]
        public void GetBorrows()
        {
            Author author = new Author
            {
                FirstName = "Wanda",
                LastName = "Maximoff"
            };

            this.session.Save(author);
            this.session.Flush();
            this.session.Clear();

            Category category = new Category
            {
                Name = "Adventure"
            };

            this.session.Save(category);
            this.session.Flush();
            this.session.Clear();

            Book book1 = new Book
            {
                Author = author,
                Category = category,
                Description = "A description",
                Price = 1000.0,
                Title = "A title",
                ISBN = "123-456-7890"
            };

            this.session.Save(book1);
            this.session.Flush();
            this.session.Clear();

            Book book2 = new Book
            {
                Author = author,
                Category = category,
                Description = "A description",
                Price = 1000.0,
                Title = "A title",
                ISBN = "123-456-7890"
            };

            this.session.Save(book2);
            this.session.Flush();
            this.session.Clear();

            Partner partner = new Partner
            {
                Username = "elonmusk",
                FirstName = "Elon",
                LastName = "Musk"
            };

            this.session.Save(partner);
            this.session.Flush();
            this.session.Clear();

            Borrow borrow1 = new Borrow
            {
                Book = book1,
                Partner = partner,
                BorrowedAt = DateTime.Now,
                ReturnedAt = DateTime.Now.AddDays(2)
            };

            Borrow borrow2 = new Borrow
            {
                Book = book2,
                Partner = partner,
                BorrowedAt = DateTime.Now,
                ReturnedAt = DateTime.Now.AddDays(2)
            };

            partner.Borrows.Add(borrow1);
            partner.Borrows.Add(borrow2);

            this.session.SaveOrUpdate(partner);
            this.session.Flush();
            this.session.Clear();

            Partner createdPartner = this.session.Get<Partner>(partner.Id);

            Assert.IsNotNull(createdPartner);
            Assert.IsNotNull(createdPartner.Borrows);
            Assert.AreEqual(2, createdPartner.Borrows.Count());
        }
    }
}