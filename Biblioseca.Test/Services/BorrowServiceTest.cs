using System;
using System.Collections.Generic;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Borrows;
using Biblioseca.DataAccess.Partners;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;
using Biblioseca.Service;
using Moq;
using NHibernate;
using NUnit.Framework;

namespace Biblioseca.Test.Services
{
    [TestFixture]
    public class BorrowServiceTest
    {
        [SetUp]
        public void SetUp()
        {
            sessionFactory = new Mock<ISessionFactory>();
            session = new Mock<ISession>();
            borrowDao = new Mock<BorrowDao>(sessionFactory.Object);
            bookDao = new Mock<BookDao>(sessionFactory.Object);
            partnerDao = new Mock<PartnerDao>(sessionFactory.Object);
        }

        private BorrowService borrowService;
        private Mock<BorrowDao> borrowDao;
        private Mock<BookDao> bookDao;
        private Mock<PartnerDao> partnerDao;
        private Mock<ISessionFactory> sessionFactory;
        private Mock<ISession> session;

        [Test]
        public void BorrowABook()
        {
            const int bookId = 1;
            const int partnerId = 1;

            bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook());
            partnerDao.Setup(dao => dao.Get(partnerId)).Returns(GetPartner());
            borrowDao.Setup(dao => dao.GetBorrows(bookId, partnerId)).Returns(new List<Borrow>());
            session.Setup(x => x.Save(It.IsAny<object>()));
            borrowDao.Setup(dao => dao.Session).Returns(session.Object);

            borrowService = new BorrowService(borrowDao.Object, bookDao.Object, partnerDao.Object);

            Borrow borrow = borrowService.BorrowABook(bookId, partnerId);

            Assert.IsNotNull(borrow);
        }

        [Test]
        public void BorrowABookWhenBookDoesNotExist()
        {
            const int bookId = 1;
            const int partnerId = 1;

            bookDao.Setup(dao => dao.Get(bookId)).Returns(default(Book));
            borrowService = new BorrowService(borrowDao.Object, bookDao.Object, partnerDao.Object);
            Assert.Throws(typeof(BusinessRuleException), () => borrowService.BorrowABook(bookId, partnerId),
                "Libro no existe. ");
        }

        [Test]
        public void BorrowABookWhenPartnerDoesNotExist()
        {
            const int bookId = 1;
            const int partnerId = 1;

            bookDao.Setup(dao => dao.Get(bookId)).Returns(default(Book));
            partnerDao.Setup(dao => dao.Get(partnerId)).Returns(default(Partner));
            borrowService = new BorrowService(borrowDao.Object, bookDao.Object, partnerDao.Object);
            Assert.Throws(typeof(BusinessRuleException), () => borrowService.BorrowABook(bookId, partnerId),
                "Socio no existe. ");
        }

        [Test]
        public void BorrowABookWhenBooksWasBorrowed()
        {
            const int bookId = 1;
            const int partnerId = 1;

            bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook());
            partnerDao.Setup(dao => dao.Get(partnerId)).Returns(GetPartner());
            borrowDao.Setup(dao => dao.GetBorrows(bookId, partnerId)).Returns(GetBorrows());
            session.Setup(x => x.Save(It.IsAny<object>()));
            borrowDao.Setup(dao => dao.Session).Returns(session.Object);

            borrowService = new BorrowService(borrowDao.Object, bookDao.Object, partnerDao.Object);

            Assert.Throws(typeof(BusinessRuleException), () => borrowService.BorrowABook(bookId, partnerId),
                "El socio no puede pedir más prestamos. El socio no puede pedir más prestamos. ");
        }

        [Test]
        public void ReturnsABook()
        {
            const int bookId = 1;
            const int partnerId = 1;

            bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook());
            partnerDao.Setup(dao => dao.Get(partnerId)).Returns(GetPartner());
            borrowDao.Setup(dao => dao.GetBorrow(bookId, partnerId)).Returns(GetBorrow());
            session.Setup(x => x.Save(It.IsAny<object>()));
            borrowDao.Setup(dao => dao.Session).Returns(session.Object);

            borrowService = new BorrowService(borrowDao.Object, bookDao.Object, partnerDao.Object);

            bool returned = borrowService.Returns(bookId, partnerId);

            Assert.IsTrue(returned);
        }

        private static Borrow GetBorrow()
        {
            return new Borrow
            {
                Id = 1,
                ReturnedAt = DateTime.Now,
                Book = new Book
                {
                    Id = 1,
                    Stock = 1
                }
            };
        }

        private static IEnumerable<Borrow> GetBorrows()
        {
            List<Borrow> borrows = new List<Borrow>
            {
                new Borrow
                {
                    Id = 1,
                    ReturnedAt = DateTime.Now
                },
                new Borrow
                {
                    Id = 2
                },
                new Borrow
                {
                    Id = 3
                }
            };

            return borrows;
        }

        private static Partner GetPartner()
        {
            Partner partner = new Partner
            {
                FirstName = "John",
                LastName = "Smith",
                Username = "johnsmith"
            };

            return partner;
        }

        private static Book GetBook()
        {
            Book book = new Book
            {
                Title = "A title",
                Description = "A description",
                Price = 1.0,
                Stock = 10
            };

            return book;
        }
    }
}