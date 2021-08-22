<%@ Page Title="Congrats" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Congrats.aspx.cs" Inherits="Biblioseca.Web.Borrows.Congrats" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>The book is yours!</h2>
    <div>
        <asp:HyperLink ID="BackToList" runat="server">Back to List</asp:HyperLink>
    </div>
</asp:Content>