using System;
using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Authors;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Categories;
using Biblioseca.Model;
using Biblioseca.Service;
using Biblioseca.Web.Common;

namespace Biblioseca.Web.Books
{
    public partial class Create : BasePage
    {
        private readonly AuthorDao authorDao = new AuthorDao(Global.SessionFactory);
        private readonly BookDao bookDao = new BookDao(Global.SessionFactory);
        private readonly CategoryDao categoryDao = new CategoryDao(Global.SessionFactory);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) BindData();
        }

        private void BindData()
        {
            BindAuthors();
            BindCategories();
        }

        private void BindCategories()
        {
            CategoryService categoryService = new CategoryService(categoryDao);
            IEnumerable<Category> categories = categoryService.GetAll();

            categoryList.DataTextField = "value";
            categoryList.DataValueField = "key";
            categoryList.DataSource = categories
                .ToDictionary(category => category.Id, category => $"{category.Name}");
            categoryList.DataBind();
        }

        private void BindAuthors()
        {
            AuthorService authorService = new AuthorService(authorDao);
            IEnumerable<Author> authors = authorService.GetAll();

            authorList.DataTextField = "value";
            authorList.DataValueField = "key";
            authorList.DataSource = authors
                .ToDictionary(author => author.Id, author => $"{author.FirstName} {author.LastName}");
            authorList.DataBind();
        }

        protected void ButtonCreateAuthor_Click(object sender, EventArgs e)
        {
            BookService bookService = new BookService(bookDao);

            CategoryService categoryService = new CategoryService(categoryDao);
            Category category = categoryService.Get(Convert.ToInt32(categoryList.SelectedValue));

            AuthorService authorService = new AuthorService(authorDao);
            Author author = authorService.Get(Convert.ToInt32(authorList.SelectedValue));

            Book book = Book.Create
            (
                textBoxTitle.Text,
                textBoxDescription.Text,
                textBoxISBN.Text,
                Convert.ToDouble(textBoxPrice.Text),
                category,
                author,
                Convert.ToInt32(textBoxStock.Text)
            );

            bookService.Create(book);

            Response.Redirect(Const.Pages.Books.List);
        }
    }
}