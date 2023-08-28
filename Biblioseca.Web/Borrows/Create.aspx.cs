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
        private readonly BorrowDao borrowDao = new BorrowDao(Global.SessionFactory);
        private readonly PartnerDao partnerDao = new PartnerDao(Global.SessionFactory);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindBooks();
                BindPartners();
                BindLinks();
            }
        }

        private void BindLinks()
        {
            BackToList.NavigateUrl = Const.Pages.Borrow.List;
            BackToList.DataBind();
        }

        private void BindPartners()
        {
            PartnerService partnerService = new PartnerService(partnerDao);
            partnerList.DataValueField = nameof(Partner.Id);
            partnerList.DataTextField = nameof(Partner.Fullname);
            partnerList.DataSource = partnerService.GetAll();
            partnerList.DataBind();
        }

        private void BindBooks()
        {
            BookService bookService = new BookService(bookDao);
            bookList.DataValueField = nameof(Book.Id);
            bookList.DataTextField = nameof(Book.Title);
            bookList.DataSource = bookService.GetAvailableBooks();
            bookList.DataBind();
        }

        protected void ButtonCreateBorrow_Click(object sender, EventArgs e)
        {
            int bookId = bookList.SelectedValue.ToInt32();
            int partnerId = partnerList.SelectedValue.ToInt32();

            BorrowService borrowService = new BorrowService(borrowDao, bookDao, partnerDao);

            BorrowDTO borrowDto = borrowService.BorrowABookForPartner(bookId, partnerId);

            if (borrowDto.HasError) Response.Redirect(Const.Pages.Borrow.BusinessError);

            Response.Redirect(Const.Pages.Borrow.Congrats);
        }
    }
}