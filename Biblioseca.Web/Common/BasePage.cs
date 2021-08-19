using System.Web;
using System.Web.UI;

namespace Biblioseca.Web.Common
{
    public class BasePage : Page
    {
        protected void PageReload()
        {
            Response.Redirect(HttpContext
                .Current
                .Request
                .AppRelativeCurrentExecutionFilePath ?? string.Empty);
        }
    }
}