<%@ Page Title="Sorry" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="BusinessError.aspx.cs" Inherits="Biblioseca.Web.Borrows.BusinessError" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>You can't check out more books</h2>
    <div>
        <asp:HyperLink ID="BackToList" runat="server">Back to List</asp:HyperLink>
    </div>
</asp:Content>