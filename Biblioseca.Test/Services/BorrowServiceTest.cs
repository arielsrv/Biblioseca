using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Borrows;
using Biblioseca.DataAccess.Partners;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;
using Biblioseca.Service;
using Moq;
using NHibernate;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Biblioseca.Test.Services
{
    [TestFixture]
    public class BorrowServiceTest
    {
        private BorrowService borrowService;
        private Mock<BorrowDao> borrowDao;
        private Mock<BookDao> bookDao;
        private Mock<PartnerDao> partnerDao;
        private Mock<ISessionFactory> sessionFactory;
        private Mock<ISession> session;

        [SetUp]
        public void SetUp()
        {
            this.sessionFactory = new Mock<ISessionFactory>();
            this.session = new Mock<ISession>();
            this.borrowDao = new Mock<BorrowDao>(this.sessionFactory.Object);
            this.bookDao = new Mock<BookDao>(this.sessionFactory.Object);
            this.partnerDao = new Mock<PartnerDao>(this.sessionFactory.Object);
        }

        [Test]
        public void BorrowABook()
        {
            const int bookId = 1;
            const int partnerId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook());
            this.partnerDao.Setup(dao => dao.Get(partnerId)).Returns(GetPartner());
            this.borrowDao.Setup(dao => dao.GetBorrows(bookId, partnerId)).Returns(new List<Borrow>());
            this.session.Setup(x => x.Save(It.IsAny<object>()));
            this.borrowDao.Setup(dao => dao.Session).Returns(this.session.Object);

            this.borrowService = new BorrowService(this.borrowDao.Object, this.bookDao.Object, this.partnerDao.Object);

            Borrow borrow = this.borrowService.BorrowABook(bookId, partnerId);

            Assert.IsNotNull(borrow);
        }

        [Test]
        public void BorrowABookWhenBookDoesNotExist()
        {
            const int bookId = 1;
            const int partnerId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(default(Book));
            this.borrowService = new BorrowService(this.borrowDao.Object, this.bookDao.Object, this.partnerDao.Object);
            Assert.Throws(typeof(BusinessRuleException), () => this.borrowService.BorrowABook(bookId, partnerId),
                "Libro no existe. ");
        }

        [Test]
        public void BorrowABookWhenPartnerDoesNotExist()
        {
            const int bookId = 1;
            const int partnerId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(default(Book));
            this.partnerDao.Setup(dao => dao.Get(partnerId)).Returns(default(Partner));
            this.borrowService = new BorrowService(this.borrowDao.Object, this.bookDao.Object, this.partnerDao.Object);
            Assert.Throws(typeof(BusinessRuleException), () => this.borrowService.BorrowABook(bookId, partnerId),
                "Socio no existe. ");
        }

        [Test]
        public void BorrowABookWhenBooksWasBorrowed()
        {
            const int bookId = 1;
            const int partnerId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook());
            this.partnerDao.Setup(dao => dao.Get(partnerId)).Returns(GetPartner());
            this.borrowDao.Setup(dao => dao.GetBorrows(bookId, partnerId)).Returns(GetBorrows());
            this.session.Setup(x => x.Save(It.IsAny<object>()));
            this.borrowDao.Setup(dao => dao.Session).Returns(this.session.Object);

            this.borrowService = new BorrowService(this.borrowDao.Object, this.bookDao.Object, this.partnerDao.Object);

            Assert.Throws(typeof(BusinessRuleException), () => this.borrowService.BorrowABook(bookId, partnerId),
                "El socio no puede pedir más prestamos. El socio no puede pedir más prestamos. ");
        }

        [Test]
        public void ReturnsABook()
        {
            const int bookId = 1;
            const int partnerId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook());
            this.partnerDao.Setup(dao => dao.Get(partnerId)).Returns(GetPartner());
            this.borrowDao.Setup(dao => dao.GetBorrow(bookId, partnerId)).Returns(GetBorrow());
            this.session.Setup(x => x.Save(It.IsAny<object>()));
            this.borrowDao.Setup(dao => dao.Session).Returns(this.session.Object);

            this.borrowService = new BorrowService(this.borrowDao.Object, this.bookDao.Object, this.partnerDao.Object);

            bool returned = this.borrowService.Returns(bookId, partnerId);

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