using Biblioseca.DataAccess.Categories;
using Biblioseca.Model;
using System.Collections.Generic;

namespace Biblioseca.Service
{
    public class CategoryService
    {
        private readonly CategoryDao categoryDao;   

        public CategoryService(CategoryDao categoryDao)
        {
            this.categoryDao = categoryDao;
        }

        public IEnumerable<Category> GetAll()
        {
            return this.categoryDao.GetAll();
        }

        public Category Get(int id)
        {
            return this.categoryDao.Get(id);
        }
    }
}