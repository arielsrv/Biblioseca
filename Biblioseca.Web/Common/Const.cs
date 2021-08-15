namespace Biblioseca.Web.Common
{
    public static class Const
    {
        public static class Pages
        {
            public static class Author
            {
                public const string List = "~/Authors/List.aspx";
                public const string Create = "~/Authors/Create.aspx";
                public const string Edit = "~/Authors/Edit.aspx?id={0}";
            }

            public static class Books
            {
                public const string List = "~/Books/List.aspx";
                public const string Create = "~/Books/Create.aspx";
            }
        }
    }
}