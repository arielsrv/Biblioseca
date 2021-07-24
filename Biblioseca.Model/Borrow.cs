using System;
using System.Collections.Generic;

namespace Biblioseca.Model
{
    public class Borrow
    {
        public virtual int Id { get; set; }
        public virtual Book Book { get; set; }
        public virtual Partner Partner { get; set; }
        public virtual DateTime BorrowedAt { get; set; }
        public virtual DateTime ReturnedAt { get; set; }
    }
}