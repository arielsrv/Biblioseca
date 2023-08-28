using System;
using System.Web;
using System.Web.UI;

namespace Biblioseca.Web
{
    public partial class Error : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Create safe error messages.
            const string generalErrorMsg = "A problem has occurred on this web site. Please try again. " +
                                           "If this error continues, please contact support.";
            const string httpErrorMsg = "An HTTP error occurred. Page Not found. Please try again.";
            const string unhandledErrorMsg = "The error was unhandled by application code.";

            // Display safe error message.
            FriendlyErrorMsg.Text = generalErrorMsg;

            // Determine where error was handled.
            string errorHandler = Request.QueryString["handler"] ?? "Error Page";

            // Get the last error from the server.
            Exception exception = Server.GetLastError();

            // Get the error number passed as a querystring value.
            string errorMsg = Request.QueryString["msg"];
            if (errorMsg == "404")
            {
                exception = new HttpException(404, httpErrorMsg, exception);
                FriendlyErrorMsg.Text = exception.Message;
            }

            // If the exception no longer exists, create a generic exception.
            if (exception == null) exception = new Exception(unhandledErrorMsg);

            // Show error details to only you (developer). LOCAL ACCESS ONLY.
            if (Request.IsLocal)
            {
                // Detailed Error Message.
                ErrorDetailedMsg.Text = exception.Message;

                // Show where the error was handled.
                ErrorHandler.Text = errorHandler;

                // Show local access details.
                DetailedErrorPanel.Visible = true;

                if (exception.InnerException != null)
                {
                    InnerMessage.Text = exception.GetType() + "<br/>" + exception.InnerException.Message;
                    InnerTrace.Text = exception.InnerException.StackTrace;
                }
                else
                {
                    InnerMessage.Text = exception.GetType().ToString();
                    if (exception.StackTrace != null) InnerTrace.Text = exception.StackTrace.TrimStart();
                }
            }

            Server.ClearError();
        }
    }
}