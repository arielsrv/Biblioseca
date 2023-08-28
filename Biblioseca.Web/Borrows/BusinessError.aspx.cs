using System;
using Biblioseca.Web.Common;

namespace Biblioseca.Web.Borrows
{
    public partial class BusinessError : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) BindLinks();
        }

        private void BindLinks()
        {
            BackToList.NavigateUrl = Const.Pages.Borrow.List;
            BackToList.DataBind();
        }
    }
}