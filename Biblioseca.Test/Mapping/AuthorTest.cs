using System;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Cfg;
using NUnit.Framework;

namespace Biblioseca.Test.Mapping
{
    [TestFixture]
    public class AuthorTests
    {
        [SetUp]
        public void SetUp()
        {
            sessionFactory = new Configuration().Configure().BuildSessionFactory();
            session = sessionFactory.OpenSession();
            transaction = session.BeginTransaction();
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
        public void CreateAuthor()
        {
            Author author = new Author
            {
                FirstName = "Wanda",
                LastName = "Maximoff"
            };

            session.Save(author);
            session.Flush();
            session.Clear();

            Assert.IsTrue(author.Id > 0);

            Author created = session.Get<Author>(author.Id);

            Assert.AreEqual(author.Id, created.Id);
        }

        [Test]
        public void GetBorrows()
        {
            Author author = new Author
            {
                FirstName = "Wanda",
                LastName = "Maximoff"
            };

            session.Save(author);
            session.Flush();
            session.Clear();

            Category category = new Category
            {
                Name = "Adventure"
            };

            session.Save(category);
            session.Flush();
            session.Clear();

            Book book1 = new Book
            {
                Author = author,
                Category = category,
                Description = "A description",
                Price = 1000.0,
                Title = "A title",
                ISBN = "123-456-7890"
            };

            session.Save(book1);
            session.Flush();
            session.Clear();

            Book book2 = new Book
            {
                Author = author,
                Category = category,
                Description = "A description",
                Price = 1000.0,
                Title = "A title",
                ISBN = "123-456-7890"
            };

            session.Save(book2);
            session.Flush();
            session.Clear();

            Partner partner = new Partner
            {
                Username = "elonmusk",
                FirstName = "Elon",
                LastName = "Musk"
            };

            session.Save(partner);
            session.Flush();
            session.Clear();

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

            session.SaveOrUpdate(partner);
            session.Flush();
            session.Clear();

            Partner createdPartner = session.Get<Partner>(partner.Id);

            Assert.IsNotNull(createdPartner);
            Assert.IsNotNull(createdPartner.Borrows);
            Assert.AreEqual(2, createdPartner.Borrows.Count);
        }
    }
}