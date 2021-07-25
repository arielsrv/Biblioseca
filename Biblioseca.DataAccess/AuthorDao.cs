using Biblioseca.Model;
using NHibernate;

namespace Biblioseca.DataAccess
{
    public class AuthorDao : Dao<Author>, IAuthorDao
    {
        public AuthorDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }
    }
}