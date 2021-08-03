using System.Collections.Generic;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Borrows;
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
        private Mock<BorrowDao> borrowDao;
        private Mock<ISessionFactory> sessionFactory;
        private Mock<ISession> session;
        
        [TestInitialize]
        public void SetUp()
        {
            this.sessionFactory = new Mock<ISessionFactory>();
            this.session = new Mock<ISession>();
            this.bookDao = new Mock<BookDao>(this.sessionFactory.Object);
            this.borrowDao = new Mock<BorrowDao>(this.sessionFactory.Object);
        }

        [TestMethod]
        public void IsAvailable()
        {
            const int bookId = 1;
            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook());
            this.borrowDao.Setup(dao => dao.GetBorrowsByBookId(bookId)).Returns(default(List<Borrow>));

            BookService bookService = new BookService(this.bookDao.Object, this.borrowDao.Object);

            bool isAvailable = bookService.IsAvailable(bookId);
            Assert.IsTrue(isAvailable);
        }
        
        [TestMethod]
        public void IsNotAvailable()
        {
            const int bookId = 1;
            this.bookDao.Setup(dao => dao.Get(1)).Returns(GetBook());
            this.borrowDao.Setup(dao => dao.GetBorrowsByBookId(bookId)).Returns(GetBorrows());

            BookService bookService = new BookService(this.bookDao.Object, this.borrowDao.Object);
            
            bool isAvailable = bookService.IsAvailable(bookId);
            Assert.IsFalse(isAvailable);
        }

        private static IEnumerable<Borrow> GetBorrows()
        {
            List<Borrow> borrows = new List<Borrow> {new Borrow {Id = 1}};

            return borrows;
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