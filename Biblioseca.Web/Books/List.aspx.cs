using Biblioseca.DataAccess.Books;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;
using Biblioseca.Service;
using Biblioseca.Web.Common;
using System;
using System.Web.UI.WebControls;

namespace Biblioseca.Web.Books
{
    public partial class List : BasePage
    {
        private readonly BookDao bookDao = new BookDao(Global.SessionFactory);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }

        private void BindGrid()
        {
            BookService bookService = new BookService(bookDao);

            this.GridViewBooks.DataSource = bookService.GetAll();
            this.GridViewBooks.DataBind();
        }

        protected void LinkCreateBook_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(Const.Pages.Books.Create);
        }

        protected void GridViewBooks_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int authorId = Convert.ToInt32(this.GridViewBooks.DataKeys[e.NewEditIndex].Values[0]);
            Response.Redirect(string.Format(Const.Pages.Author.Edit, authorId));
        }

        protected void GridViewBooks_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int bookId = Convert.ToInt32(this.GridViewBooks.DataKeys[e.RowIndex].Values[0]);
            Book book = this.bookDao.Get(bookId);
            Ensure.NotNull(book, "Libro no existe. ");
            this.bookDao.Delete(book);
            this.BindGrid();
            this.PageReload();
        }
    }
}