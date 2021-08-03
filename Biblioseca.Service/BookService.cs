using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Borrows;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;

namespace Biblioseca.Service
{
    public class BookService
    {
        private readonly BookDao bookDao;
        private readonly BorrowDao borrowDao;

        public BookService(BookDao bookDao, BorrowDao borrowDao)
        {
            this.bookDao = bookDao;
            this.borrowDao = borrowDao;
        }

        public bool IsAvailable(int bookId)
        {
            Ensure.IsTrue(bookId > 0, "Book.Id debe ser mayor que 0.");

            Book book = this.bookDao.Get(bookId);
            Ensure.NotNull(book, "Libro no existe. ");

            IEnumerable<Borrow> borrows = this.borrowDao.GetBorrowsByBookId(bookId);

            return borrows == null || !borrows.Any();
        }
    }
}