using System;
using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Borrows;
using Biblioseca.DataAccess.Partners;
using Biblioseca.Model;

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

            if (book == null)
            {
                throw new ApplicationException("Libro no existe. ");
            }

            Partner partner = partnerDao.Get(partnerId);

            if (partner == null)
            {
                throw new ApplicationException("Socio no existe. ");
            }

            if (partner.Borrows.Count == 2)
            {
                throw new ApplicationException("No puede pedir prestado libros. ");
            }

            IEnumerable<Borrow> borrows = borrowDao.GetBorrowsByBookId(bookId);

            if (borrows.Any())
            {
                throw new ApplicationException("Libro prestado. ");
            }

            Borrow newBorrow = new Borrow
            {
                Book = book,
                Partner = partner,
                BorrowedAt = DateTime.Now
            };

            borrowDao.Save(newBorrow);

            return newBorrow;
        }
    }
}