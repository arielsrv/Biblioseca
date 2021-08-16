using Biblioseca.DataAccess.Books;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;
using System.Collections.Generic;

namespace Biblioseca.Service
{
    public class BookService
    {
        private readonly BookDao bookDao;

        public BookService(BookDao bookDao)
        {
            this.bookDao = bookDao;
        }

        public bool IsAvailable(int bookId)
        {
            Ensure.IsTrue(bookId > 0, "Book.Id debe ser mayor que 0.");

            Book book = this.bookDao.Get(bookId);
            Ensure.NotNull(book, "Libro no existe. ");

            return book.Stock > 0;
        }

        public IEnumerable<Book> GetAll()
        {
            return this.bookDao.GetAll();
        }

        public void Create(Book book)
        {
            this.bookDao.Save(book);
        }
    }
}