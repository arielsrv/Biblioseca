using System.Collections.Generic;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Books.Filters;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;

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

            Book book = bookDao.Get(bookId);
            Ensure.NotNull(book, "Libro no existe. ");

            return book.Stock > 0;
        }

        public IEnumerable<Book> GetAll()
        {
            return bookDao.GetAll();
        }

        public IEnumerable<Book> GetAvailableBooks()
        {
            BookFilterDto bookFilterDto = new BookFilterDto
            {
                Stock = true
            };

            return bookDao.GetByFilter(bookFilterDto);
        }

        public void Create(Book book)
        {
            bookDao.Save(book);
        }

        public Book Get(int bookId)
        {
            return bookDao.Get(bookId);
        }
    }
}