using Biblioseca.DataAccess.Authors;
using Biblioseca.Model;
using Biblioseca.Service;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Biblioseca.Web
{
    public partial class Authors : Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthorDao authorDao = new AuthorDao(Global.SessionFactory);
            AuthorService authorService = new AuthorService(authorDao);
            
            this.GridViewAuthors.DataSource = authorService.GetAll();
            this.GridViewAuthors.DataBind();
        }
    }
}