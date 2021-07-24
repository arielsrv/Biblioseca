using System.Collections.Generic;

namespace Biblioseca.Model
{
    public class Partner
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Username { get; set; }
    }
}