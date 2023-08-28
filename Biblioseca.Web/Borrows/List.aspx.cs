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
        private readonly BookDao bookDao = new BookDao(Global.SessionFactory);
        private readonly BorrowDao borrowDao = new BorrowDao(Global.SessionFactory);
        private readonly PartnerDao partnerDao = new PartnerDao(Global.SessionFactory);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
                BindLinks();
            }
        }

        private void BindLinks()
        {
            CreateNew.NavigateUrl = Const.Pages.Borrow.Create;
            CreateNew.DataBind();
        }

        private void BindGrid()
        {
            BorrowService borrowService = new BorrowService(borrowDao, bookDao, partnerDao);

            GridViewBorrows.DataSource = borrowService.GetBorrows();
            GridViewBorrows.DataBind();
        }
    }
}