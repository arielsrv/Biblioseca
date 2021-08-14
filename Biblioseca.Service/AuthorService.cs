using Biblioseca.DataAccess.Authors;
using Biblioseca.Model;
using System.Collections.Generic;

namespace Biblioseca.Service
{
    public class AuthorService
    {
        private readonly AuthorDao authorDao;

        public AuthorService(AuthorDao authorDao)
        {
            this.authorDao = authorDao;
        }

        public IEnumerable<Author> GetAll()
        {
            return this.authorDao.GetAll();
        }

        public void Create(Author author)
        {
            this.authorDao.Save(author);
        }
    }
}