using System;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Cfg;
using NUnit.Framework;

namespace Biblioseca.Test.Mapping
{
    [TestFixture]
    public class BorrowTest
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
        public void CreateBorrow()
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

            Book book = new Book
            {
                Author = author,
                Category = category,
                Description = "A description",
                Price = 1000.0,
                Title = "A title",
                ISBN = "123-456-7890"
            };

            session.Save(book);
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

            Borrow borrow = new Borrow
            {
                Book = book,
                Partner = partner,
                BorrowedAt = DateTime.Now,
                ReturnedAt = DateTime.Now.AddDays(2)
            };

            session.Save(borrow);

            Assert.That(author.Id > 0);

            Borrow created = session.Get<Borrow>(borrow.Id);

            Assert.Equals(borrow.Id, created.Id);
            Assert.Equals(borrow.Partner.Id, partner.Id);
            Assert.Equals(borrow.Book.Id, book.Id);
        }
    }
}
