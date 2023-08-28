using System;
using Biblioseca.Model.Exceptions;

namespace Biblioseca.Model
{
    public class Borrow : Entity
    {
        public virtual Book Book { get; set; }
        public virtual Partner Partner { get; set; }
        public virtual DateTime? BorrowedAt { get; set; }
        public virtual DateTime? ReturnedAt { get; set; }

        /// <summary>
        ///     Creates the specified book.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <param name="partner">The partner.</param>
        /// <returns></returns>
        public static Borrow Create(Book book, Partner partner)
        {
            Ensure.NotNull(book, "Borrow.Book no puede ser nulo. ");
            Ensure.NotNull(partner, "Borrow.Partner no puede ser nulo. ");

            Borrow borrow = new Borrow
            {
                Book = book,
                Partner = partner,
                BorrowedAt = DateTime.Now
            };

            return borrow;
        }

        public virtual void Returned()
        {
            ReturnedAt = DateTime.Now;
        }
    }
}