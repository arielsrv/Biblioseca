﻿using System;
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

            List<int> values1 = new List<int>();
            values1.Add(1);

            List<string> values2 = new List<string>();
            values2.Add("hello world!");

            Console.ReadKey();
        }
    }
}