using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace Biblioseca.DataAccess
{
    public abstract class Dao<T> : IDao<T>
    {
        private readonly ISessionFactory sessionFactory;

        protected Dao(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        protected ISession GetCurrentSession()
        {
            return this.sessionFactory
                .GetCurrentSession();
        }

        public void Save(T entity)
        {
            this.GetCurrentSession()
                .Save(entity);
        }

        public void Delete(T entity)
        {
            this.GetCurrentSession()
                .Delete(entity);
        }

        public T Get(int id)
        {
            return this.GetCurrentSession()
                .Get<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            return this.GetCurrentSession()
                .Query<T>();
        }

        public T GetUniqueByQuery(string queryString, IDictionary<string, object> parameters)
        {
            IQuery query = this.GetCurrentSession()
                .CreateQuery(queryString);

            foreach (KeyValuePair<string, object> keyValue in parameters)
            {
                query.SetParameter(keyValue.Key, keyValue.Value);
            }

            return query.UniqueResult<T>();
        }

        public T GetUniqueByQuery(IDictionary<string, object> parameters)
        {
            ICriteria criteria = this.GetCurrentSession()
                .CreateCriteria(typeof(T));

            foreach (KeyValuePair<string, object> keyValue in parameters)
            {
                criteria.Add(Restrictions.Eq(keyValue.Key, keyValue.Value));
            }

            return criteria.UniqueResult<T>();
        }
    }
}