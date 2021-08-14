using Biblioseca.DataAccess.Authors;
using Biblioseca.Model;
using Biblioseca.Service;
using Biblioseca.Web.Common;
using System;

namespace Biblioseca.Web
{
    public partial class CreateNewAuthor : BasePage
    {
        private readonly AuthorDao authorDao = new AuthorDao(Global.SessionFactory);

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ButtonCreateAuthor_Click(object sender, EventArgs e)
        {
            AuthorService authorService = new AuthorService(authorDao);

            Author author = Author
                .Create(textBoxFirstName.Text, textBoxLastName.Text);

            authorService.Create(author);

            Response.Redirect(Const.Pages.Author.List);
        }
    }
}