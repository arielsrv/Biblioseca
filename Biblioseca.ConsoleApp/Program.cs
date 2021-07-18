using Biblioseca.Model;
using NHibernate;
using NHibernate.Cfg;
using System;

namespace Biblioseca.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ISessionFactory sessionFactory = new Configuration()
                .Configure()
                .BuildSessionFactory();

            ISession session = sessionFactory.OpenSession();

            Category category = session.Get<Category>(4);
            Author author = session.Get<Author>(5);

            Book book = new Book
            {
                ISBN = "ASD",
                Author = author,
                Category = category,
                Description = "description",
                Title = "a title",
                Price = 102.0
            };

            session.Save(book);

            Console.ReadKey();
        }
    }
}