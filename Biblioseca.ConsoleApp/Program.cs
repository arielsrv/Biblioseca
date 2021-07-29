using System;
using System.Collections.Generic;
using Biblioseca.DataAccess.Authors;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace Biblioseca.ConsoleApp
{
    public static class Program
    {
        private static void Main()
        {
            ISessionFactory sessionFactory = new Configuration()
                .Configure()
                .BuildSessionFactory();

            ISession session = sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            AuthorDao authorDao = new AuthorDao(sessionFactory);

            IEnumerable<Author> authors = authorDao.GetAll();

            foreach (Author author in authors)
            {
                Console.WriteLine($"Author: {author.FirstName}, {author.LastName}");
            }
        }
    }
}