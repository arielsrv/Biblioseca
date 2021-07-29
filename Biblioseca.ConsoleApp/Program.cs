using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;

namespace Biblioseca.ConsoleApp
{
    public class Program
    {
        private static void Main()
        {
            ISessionFactory sessionFactory = new Configuration()
                .Configure()
                .BuildSessionFactory();

            ISession session = sessionFactory.OpenSession();
            session.Close();
            
            Console.ReadKey();
        }
    }
}