using System;
using System.Web.UI;
using Biblioseca.Web.Common;

namespace Biblioseca.Web.Borrows
{
    public partial class Congrats : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BindLinks();
            }
        }

        private void BindLinks()
        {
            this.BackToList.NavigateUrl = Const.Pages.Borrow.List;
            this.BackToList.DataBind();
        }
    }
}