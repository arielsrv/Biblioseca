using System;
using System.Collections.Generic;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Borrows;
using Biblioseca.DataAccess.Partners;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;
using Biblioseca.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;

namespace Biblioseca.Test.Services
{
    [TestClass]
    public class BorrowServiceTest
    {
        private BorrowService borrowService;
        private Mock<BorrowDao> borrowDao;
        private Mock<BookDao> bookDao;
        private Mock<PartnerDao> partnerDao;
        private Mock<ISessionFactory> sessionFactory;
        private Mock<ISession> session;

        [TestInitialize]
        public void SetUp()
        {
            this.sessionFactory = new Mock<ISessionFactory>();
            this.session = new Mock<ISession>();
            this.borrowDao = new Mock<BorrowDao>(this.sessionFactory.Object);
            this.bookDao = new Mock<BookDao>(this.sessionFactory.Object);
            this.partnerDao = new Mock<PartnerDao>(this.sessionFactory.Object);
        }

        [TestMethod]
        public void BorrowABook()
        {
            const int bookId = 1;
            const int partnerId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook());
            this.partnerDao.Setup(dao => dao.Get(partnerId)).Returns(GetPartner());
            this.borrowDao.Setup(dao => dao.GetBorrowsByBookId(bookId)).Returns(new List<Borrow>());
            this.session.Setup(x => x.Save(It.IsAny<object>()));
            this.borrowDao.Setup(dao => dao.Session).Returns(this.session.Object);

            this.borrowService = new BorrowService(this.borrowDao.Object, this.bookDao.Object, this.partnerDao.Object);

            Borrow borrow = this.borrowService.BorrowABook(bookId, partnerId);

            Assert.IsNotNull(borrow);
        }

        [TestMethod]
        public void BorrowABookWhenBookDoesNotExist()
        {
            const int bookId = 1;
            const int partnerId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(default(Book));
            this.borrowService = new BorrowService(this.borrowDao.Object, this.bookDao.Object, this.partnerDao.Object);
            Assert.ThrowsException<BusinessRuleException>(() => this.borrowService.BorrowABook(bookId, partnerId),
                "Libro no existe. ");
        }


        [TestMethod]
        public void BorrowABookWhenPartnerDoesNotExist()
        {
            const int bookId = 1;
            const int partnerId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(default(Book));
            this.partnerDao.Setup(dao => dao.Get(partnerId)).Returns(default(Partner));
            this.borrowService = new BorrowService(this.borrowDao.Object, this.bookDao.Object, this.partnerDao.Object);
            Assert.ThrowsException<BusinessRuleException>(() => this.borrowService.BorrowABook(bookId, partnerId),
                "Socio no existe. ");
        }

        [TestMethod]
        public void BorrowABookWhenBooksWasBorrowed()
        {
            const int bookId = 1;
            const int partnerId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook());
            this.partnerDao.Setup(dao => dao.Get(partnerId)).Returns(GetPartner());
            this.borrowDao.Setup(dao => dao.GetBorrowsByBookId(bookId)).Returns(GetBorrows());
            this.session.Setup(x => x.Save(It.IsAny<object>()));
            this.borrowDao.Setup(dao => dao.Session).Returns(this.session.Object);

            this.borrowService = new BorrowService(this.borrowDao.Object, this.bookDao.Object, this.partnerDao.Object);

            Assert.ThrowsException<BusinessRuleException>(() => this.borrowService.BorrowABook(bookId, partnerId),
                "El libro ya fue prestado. ");
        }

        private static IEnumerable<Borrow> GetBorrows()
        {
            List<Borrow> borrows = new List<Borrow> {new Borrow {Id = 1}};

            return borrows;
        }

        private static Partner GetPartner()
        {
            Partner partner = new Partner()
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
                Price = 1.0
            };

            return book;
        }
    }
}