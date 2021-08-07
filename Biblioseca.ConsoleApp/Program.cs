using System;
using System.Collections.Generic;
using Biblioseca.DataAccess.Authors;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Borrows;
using Biblioseca.DataAccess.Partners;
using Biblioseca.Model;
using Biblioseca.Service;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace Biblioseca.ConsoleApp
{
    public static class Program
    {
        // private static void Main()
        // {
        //     ISessionFactory sessionFactory = new Configuration()
        //         .Configure()
        //         .BuildSessionFactory();
        // 
        //     ISession session = sessionFactory.OpenSession();
        //     CurrentSessionContext.Bind(session);
        // 
        //     BorrowDao borrowDao = new BorrowDao(sessionFactory);
        //     BookDao bookDao = new BookDao(sessionFactory);
        //     PartnerDao partnerDao = new PartnerDao(sessionFactory);
        // 
        //     BorrowService borrowService = new BorrowService(borrowDao, bookDao, partnerDao);
        //             
        //     borrowService.BorrowABook(527, 1);
        //     
        //     // session.Flush();
        // }
    }
}