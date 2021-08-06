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
            Ensure.IsTrue(book.Stock > 0, "No hay stock disponible. ");
            
            Partner partner = partnerDao.Get(partnerId);
            Ensure.NotNull(partner, "Socio no existe. ");
            
            IEnumerable<Borrow> borrows = borrowDao.GetBorrows(partnerId);
            Ensure.IsTrue(borrows.Count() < 2, "El socio no puede pedir más prestamos. ");

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