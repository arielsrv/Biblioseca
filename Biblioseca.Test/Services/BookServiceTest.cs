using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Books.Filters;
using Biblioseca.Model;
using Biblioseca.Service;
using Moq;
using NHibernate;
using NUnit.Framework;

namespace Biblioseca.Test.Services
{
    [TestFixture]
    public class BookServiceTest
    {
        [SetUp]
        public void SetUp()
        {
            sessionFactory = new Mock<ISessionFactory>();
            bookDao = new Mock<BookDao>(sessionFactory.Object);
        }

        private Mock<BookDao> bookDao;
        private Mock<ISessionFactory> sessionFactory;

        [Test]
        public void IsAvailable()
        {
            const int bookId = 1;
            bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook(1));

            BookService bookService = new BookService(bookDao.Object);

            bool isAvailable = bookService.IsAvailable(bookId);
            Assert.That(isAvailable);
        }

        [Test]
        public void IsNotAvailable()
        {
            const int bookId = 1;
            bookDao.Setup(dao => dao.Get(1)).Returns(GetBook(0));

            BookService bookService = new BookService(bookDao.Object);

            bool isAvailable = bookService.IsAvailable(bookId);
            Assert.That(isAvailable);
        }

        [Test]
        public void GetAvailableBooks()
        {
            bookDao.Setup(dao => dao.GetByFilter(It.IsAny<BookFilterDto>())).Returns(GetBooks());

            BookService bookService = new BookService(bookDao.Object);

            IEnumerable<Book> books = bookService.GetAvailableBooks();

            Assert.That(books.Any());
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
