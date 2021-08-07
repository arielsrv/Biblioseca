using System;

namespace Biblioseca.Model
{
    public class Borrow : Entity
    {
        public virtual Book Book { get; set; }
        public virtual Partner Partner { get; set; }
        public virtual DateTime? BorrowedAt { get; set; }
        public virtual DateTime? ReturnedAt { get; set; }

        public static Borrow Create(Book book, Partner partner)
        {
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