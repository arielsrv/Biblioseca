using System.Collections.Generic;
using Biblioseca.DataAccess.Authors;
using Biblioseca.Model;

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
            return authorDao.GetAll();
        }

        public void Create(Author author)
        {
            authorDao.Save(author);
        }

        public Author Get(int authorId)
        {
            return authorDao.Get(authorId);
        }

        public void Update(Author author)
        {
            authorDao.Save(author);
        }
    }
}