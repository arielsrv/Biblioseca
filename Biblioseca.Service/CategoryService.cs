using System.Collections.Generic;
using Biblioseca.DataAccess.Categories;
using Biblioseca.Model;

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
            return categoryDao.GetAll();
        }

        public Category Get(int id)
        {
            return categoryDao.Get(id);
        }
    }
}