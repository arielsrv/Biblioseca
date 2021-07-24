using System;

namespace Biblioseca.Model
{
    public class Borrow : Entity
    {
        public virtual Book Book { get; set; }
        public virtual Partner Partner { get; set; }
        public virtual DateTime BorrowedAt { get; set; }
        public virtual DateTime ReturnedAt { get; set; }
    }
}