using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Borrows;
using Biblioseca.DataAccess.Partners;
using Biblioseca.Model;
using Biblioseca.Model.Exceptions;
using Biblioseca.Service;
using static System.Convert;
using static Biblioseca.Web.Common.Const;

namespace Biblioseca.Web.Borrows
{
    public partial class Create : Page
    {
        private readonly BookDao bookDao = new BookDao(Global.SessionFactory);
        private readonly PartnerDao partnerDao = new PartnerDao(Global.SessionFactory);
        private readonly BorrowDao borrowDao = new BorrowDao(Global.SessionFactory);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BindBooks();
                this.BindPartners();
                this.BindLinks();
            }
        }

        private void BindLinks()
        {
            this.BackToList.NavigateUrl = Pages.Borrow.List;
            this.BackToList.DataBind();
        }

        private void BindPartners()
        {
            PartnerService partnerService = new PartnerService(this.partnerDao);
            IEnumerable<Partner> partners = partnerService.GetAll();

            this.partnerList.DataValueField = "key";
            this.partnerList.DataTextField = "value";
            this.partnerList.DataSource =
                partners.ToDictionary(partner => partner.Id, partner => $"{partner.Fullname}");
            this.partnerList.DataBind();
        }

        private void BindBooks()
        {
            BookService bookService = new BookService(this.bookDao);
            IEnumerable<Book> books = bookService.GetAvailableBooks();

            this.bookList.DataValueField = "key";
            this.bookList.DataTextField = "value";
            this.bookList.DataSource =
                books.ToDictionary(book => book.Id, book => $"{book.Title}");
            this.bookList.DataBind();
        }

        protected void ButtonCreateBorrow_Click(object sender, EventArgs e)
        {
            int bookId = ToInt32(this.bookList.SelectedValue);
            int partnerId = ToInt32(this.partnerList.SelectedValue);

            BorrowService borrowService = new BorrowService(this.borrowDao, this.bookDao, this.partnerDao);

            BorrowDTO result = borrowService.BorrowABookForPartner(bookId, partnerId);

            if (result.HasError)
            {
                Response.Redirect(Pages.Borrow.BusinessError);
            }

            Response.Redirect(Pages.Borrow.Congrats);
        }
    }
}