using Biblioseca.DataAccess.Books;
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
            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook());

            BookService bookService = new BookService(this.bookDao.Object);
            
            bool isAvailable = bookService.IsAvailable(bookId);
            Assert.IsTrue(isAvailable);
        }
        
        [TestMethod]
        public void IsNotAvailable()
        {
            const int bookId = 1;
            this.bookDao.Setup(dao => dao.Get(2)).Returns(GetBook());

            BookService bookService = new BookService(this.bookDao.Object);
            
            bool isAvailable = bookService.IsAvailable(bookId);
            Assert.IsFalse(isAvailable);
        }
        
        private static Book GetBook()
        {
            Book book = new Book
            {
                Title = "A title",
                Description = "A description",
                Price = 1.0
            };

            return book;
        }
    }
}