using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Books.Filters;
using Biblioseca.Model;
using Biblioseca.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;

namespace Biblioseca.Test.Services
{
    [TestClass]
    public class BookServiceTest
    {
        private Mock<BookDao> bookDao;
        private Mock<ISessionFactory> sessionFactory;
        private Mock<ISession> session;

        [TestInitialize]
        public void SetUp()
        {
            this.sessionFactory = new Mock<ISessionFactory>();
            this.session = new Mock<ISession>();
            this.bookDao = new Mock<BookDao>(this.sessionFactory.Object);
        }

        [TestMethod]
        public void IsAvailable()
        {
            const int bookId = 1;
            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook(1));

            BookService bookService = new BookService(this.bookDao.Object);

            bool isAvailable = bookService.IsAvailable(bookId);
            Assert.IsTrue(isAvailable);
        }

        [TestMethod]
        public void IsNotAvailable()
        {
            const int bookId = 1;
            this.bookDao.Setup(dao => dao.Get(1)).Returns(GetBook(0));

            BookService bookService = new BookService(this.bookDao.Object);

            bool isAvailable = bookService.IsAvailable(bookId);
            Assert.IsFalse(isAvailable);
        }

        [TestMethod]
        public void GetAvailableBooks()
        {
            this.bookDao.Setup(dao => dao.GetByFilter(It.IsAny<BookFilterDto>())).Returns(GetBooks());

            BookService bookService = new BookService(this.bookDao.Object);

            IEnumerable<Book> books = bookService.GetAvailableBooks();
            
            Assert.IsTrue(books.Any());
        }

        private static IEnumerable<Book> GetBooks()
        {
            List<Book> books = new List<Book>
            {
                new Book
                {
                    Title = "A title",
                    Stock = 0
                },
                new Book
                {
                    Title = "Another title",
                    Stock = 2
                }
            };

            return books;
        }

        private static Book GetBook(int stock)
        {
            Book book = new Book
            {
                Title = "A title",
                Description = "A description",
                Price = 1.0,
                Stock = stock
            };

            return book;
        }
    }
}