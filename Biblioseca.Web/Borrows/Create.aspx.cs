using System;
using System.Web.UI;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Borrows;
using Biblioseca.DataAccess.Partners;
using Biblioseca.Model;
using Biblioseca.Service;
using Biblioseca.Web.Common;

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
            this.BackToList.NavigateUrl = Const.Pages.Borrow.List;
            this.BackToList.DataBind();
        }

        private void BindPartners()
        {
            PartnerService partnerService = new PartnerService(this.partnerDao);
            this.partnerList.DataValueField = nameof(Partner.Id);
            this.partnerList.DataTextField = nameof(Partner.Fullname);
            this.partnerList.DataSource = partnerService.GetAll();
            this.partnerList.DataBind();
        }

        private void BindBooks()
        {
            BookService bookService = new BookService(this.bookDao);
            this.bookList.DataValueField = nameof(Book.Id);
            this.bookList.DataTextField = nameof(Book.Title);
            this.bookList.DataSource = bookService.GetAvailableBooks();
            this.bookList.DataBind();
        }

        protected void ButtonCreateBorrow_Click(object sender, EventArgs e)
        {
            int bookId = this.bookList.SelectedValue.ToInt32();
            int partnerId = this.partnerList.SelectedValue.ToInt32();

            BorrowService borrowService = new BorrowService(this.borrowDao, this.bookDao, this.partnerDao);

            BorrowDTO borrowDto = borrowService.BorrowABookForPartner(bookId, partnerId);

            if (borrowDto.HasError)
            {
                Response.Redirect(Const.Pages.Borrow.BusinessError);
            }

            Response.Redirect(Const.Pages.Borrow.Congrats);
        }
    }
}