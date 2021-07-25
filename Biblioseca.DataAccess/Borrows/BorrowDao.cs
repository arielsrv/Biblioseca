using System.Collections.Generic;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Biblioseca.DataAccess.Borrows
{
    public class BorrowDao : Dao<Borrow>, IBorrowDao
    {
        public BorrowDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public virtual IEnumerable<Borrow> GetBorrowsByBookId(int bookId)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Borrow>();
            
            criteria.CreateCriteria("Book")
                .Add(Restrictions.Eq("Id", bookId));
            
            return criteria.List<Borrow>();
        }
    }
}