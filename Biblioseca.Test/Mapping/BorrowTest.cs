using Biblioseca.Model;
using NHibernate;
using NHibernate.Cfg;
using NUnit.Framework;
using System;

namespace Biblioseca.Test.Mapping
{
    [TestFixture]
    public class BorrowTest
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
        }

        [TearDown]
        public void CleanUp()
        {
            this.transaction.Rollback();
            this.session.Close();
        }

        [Test]
        public void CreateBorrow()
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

            Book book = new Book
            {
                Author = author,
                Category = category,
                Description = "A description",
                Price = 1000.0,
                Title = "A title",
                ISBN = "123-456-7890"
            };

            this.session.Save(book);
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

            Borrow borrow = new Borrow
            {
                Book = book,
                Partner = partner,
                BorrowedAt = DateTime.Now,
                ReturnedAt = DateTime.Now.AddDays(2)
            };

            this.session.Save(borrow);

            Assert.IsTrue(author.Id > 0);

            Borrow created = this.session.Get<Borrow>(borrow.Id);

            Assert.AreEqual(borrow.Id, created.Id);
            Assert.AreEqual(borrow.Partner.Id, partner.Id);
            Assert.AreEqual(borrow.Book.Id, book.Id);
        }
    }
}