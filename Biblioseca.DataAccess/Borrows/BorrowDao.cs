using Biblioseca.Model;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;

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

        public virtual IEnumerable<Borrow> GetBorrows(int bookId, int partnerId)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Borrow>();

            criteria.CreateCriteria("Book")
                .Add(Restrictions.Eq("Id", bookId));

            criteria.CreateCriteria("Partner")
                .Add(Restrictions.Eq("Id", partnerId));

            criteria.Add(Restrictions.Eq("ReturnedAt", null));

            return criteria.List<Borrow>();
        }

        public virtual Borrow GetBorrow(int bookId, int partnerId)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Borrow>();

            criteria.CreateCriteria("Book")
                .Add(Restrictions.Eq("Id", bookId));

            criteria.CreateCriteria("Partner")
                .Add(Restrictions.Eq("Id", partnerId));

            criteria.Add(Restrictions.Eq("ReturnedAt", null));

            return criteria.UniqueResult<Borrow>();
        }
    }
}