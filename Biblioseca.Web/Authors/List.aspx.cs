using Biblioseca.DataAccess.Authors;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;
using Biblioseca.Service;
using Biblioseca.Web.Common;
using System;
using System.Web.UI.WebControls;

namespace Biblioseca.Web
{
    public partial class List : BasePage
    {
        private readonly AuthorDao authorDao = new AuthorDao(Global.SessionFactory);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }

        private void BindGrid()
        {
            AuthorService authorService = new AuthorService(authorDao);

            this.GridViewAuthors.DataSource = authorService.GetAll();
            this.GridViewAuthors.DataBind();
        }

        protected void LinkCreateNewAuthor_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(Const.Pages.Author.Create);
        }

        protected void GridViewAuthors_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int authorId = Convert.ToInt32(this.GridViewAuthors.DataKeys[e.NewEditIndex].Values[0]);
            Response.Redirect(string.Format(Const.Pages.Author.Edit, authorId));
        }

        protected void GridViewAuthors_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int authorId = Convert.ToInt32(this.GridViewAuthors.DataKeys[e.RowIndex].Values[0]);
            Author author = this.authorDao.Get(authorId);
            Ensure.NotNull(author, "Author no existe. ");
            this.authorDao.Delete(author);
            this.BindGrid();
            this.PageReload();
        }
    }
}