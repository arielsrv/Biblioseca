using System;
using Biblioseca.DataAccess.Authors;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;
using Biblioseca.Service;
using Biblioseca.Web.Common;

namespace Biblioseca.Web.Authors
{
    public partial class Edit : BasePage
    {
        private readonly AuthorDao authorDao = new AuthorDao(Global.SessionFactory);
        private int authorId;

        protected void Page_Load(object sender, EventArgs e)
        {
            authorId = Convert.ToInt32(Request.QueryString.Get("id"));

            if (!IsPostBack) BindAuthor();
        }

        private void BindAuthor()
        {
            AuthorService authorService = new AuthorService(authorDao);
            Author author = authorService.Get(authorId);
            Ensure.NotNull(author, "Author no existe. ");

            textBoxFirstName.Text = author.FirstName;
            textBoxLastName.Text = author.LastName;
        }

        protected void ButtonEditAuthor_Click(object sender, EventArgs e)
        {
            AuthorService authorService = new AuthorService(authorDao);
            Author author = authorService.Get(authorId);
            Ensure.NotNull(author, "Author no existe. ");

            author.SetFirstName(textBoxFirstName.Text);
            author.SetLastName(textBoxLastName.Text);

            authorService.Update(author);

            Response.Redirect(Const.Pages.Author.List);
        }
    }
}