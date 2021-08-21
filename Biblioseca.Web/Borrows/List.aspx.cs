using System;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Borrows;
using Biblioseca.DataAccess.Partners;
using Biblioseca.Service;
using Biblioseca.Web.Common;

namespace Biblioseca.Web.Borrows
{
    public partial class List : BasePage
    {
        private readonly BorrowDao borrowDao = new BorrowDao(Global.SessionFactory);
        private readonly BookDao bookDao = new BookDao(Global.SessionFactory);
        private readonly PartnerDao partnerDao = new PartnerDao(Global.SessionFactory);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BindGrid();
                this.BindLinks();
            }
        }

        private void BindLinks()
        {
            this.CreateNew.NavigateUrl = Const.Pages.Borrow.Create;
            this.CreateNew.DataBind();
        }

        private void BindGrid()
        {
            BorrowService borrowService = new BorrowService(this.borrowDao, this.bookDao, this.partnerDao);

            this.GridViewBorrows.DataSource = borrowService.GetBorrows();
            this.GridViewBorrows.DataBind();
        }
    }
}