using System.Collections.Generic;

namespace Biblioseca.Model
{
    public class Partner : Entity
    {
        public Partner()
        {
            Borrows = new HashSet<Borrow>();
        }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Username { get; set; }
        public virtual ISet<Borrow> Borrows { get; set; }
    }
}