using Biblioseca.DataAccess.Authors;
using Biblioseca.DataAccess.Books;
using Biblioseca.Model;
using Biblioseca.Service;
using Biblioseca.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biblioseca.Web.Books
{
    public partial class Create : BasePage
    {
        private readonly BookDao bookDao = new BookDao(Global.SessionFactory);
        private readonly AuthorDao authorDao = new AuthorDao(Global.SessionFactory);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindData();
            }
        }

        private void BindData()
        {
            AuthorService authorService = new AuthorService(authorDao);
            IEnumerable<Author> authors = authorService.GetAll();

            this.authorList.DataTextField = "value";
            this.authorList.DataValueField = "key";
            this.authorList.DataSource = authors
                .ToDictionary(author => author.Id, author => $"{author.FirstName} {author.LastName}");
            this.authorList.DataBind();
        }

        protected void ButtonCreateAuthor_Click(object sender, EventArgs e)
        {
        }
    }
}