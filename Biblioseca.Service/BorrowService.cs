using System;
using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Borrows;
using Biblioseca.DataAccess.Partners;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;

namespace Biblioseca.Service
{
    public class BorrowService
    {
        private readonly BorrowDao borrowDao;
        private readonly BookDao bookDao;
        private readonly PartnerDao partnerDao;

        public BorrowService(BorrowDao borrowDao, BookDao bookDao, PartnerDao partnerDao)
        {
            this.borrowDao = borrowDao;
            this.bookDao = bookDao;
            this.partnerDao = partnerDao;
        }

        public Borrow BorrowABook(int bookId, int partnerId)
        {
            Book book = bookDao.Get(bookId);
            Ensure.NotNull(book, "Libro no existe. ");

            Partner partner = partnerDao.Get(partnerId);
            Ensure.NotNull(partner, "Socio no existe. ");
            Ensure.IsTrue(partner.Borrows.Count < 2, "No puede pedir prestado más libros. ");

            IEnumerable<Borrow> borrows = borrowDao.GetBorrowsByBookId(bookId);
            Ensure.IsTrue(!borrows.Any(), "El libro ya fue prestado. ");

            Borrow borrow = new Borrow
            {
                Book = book,
                Partner = partner,
                BorrowedAt = DateTime.Now
            };

            borrowDao.Save(borrow);

            return borrow;
        }
    }
}