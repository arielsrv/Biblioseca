using Biblioseca.Model;
using NHibernate;

namespace Biblioseca.DataAccess.Partners
{
    public class PartnerDao : Dao<Partner>, IPartnerDao
    {
        public PartnerDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }
    }
}