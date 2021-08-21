<%@ Page Title="Borrows" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Biblioseca.Web.Borrows.List" %>
<%@ Import Namespace="Biblioseca.Web.Common" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>List</h2>
    <p>
        <asp:HyperLink ID="CreateNew" runat="server">Create New</asp:HyperLink>
    </p>
    <asp:GridView ID="GridViewBorrows" runat="server" AutoGenerateColumns="false" DataKeyNames="Id" CssClass="table table-bordered table-hover">
        <Columns>
            <asp:TemplateField HeaderText="Title">
                <ItemTemplate>
                    <asp:Label ID="labelBookTitle" runat="server" Text='<%# Eval("Book.Title") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Partner">
                <ItemTemplate>
                    <asp:Label ID="labelPartnerFirstName" runat="server" Text='<%# Eval("Partner.Fullname") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Borrowed At">
                <ItemTemplate>
                    <asp:Label ID="labelBorrowedAt" runat="server" Text='<%# Eval("BorrowedAt") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Returned At">
                <ItemTemplate>
                    <asp:Label ID="labelReturnedAt" runat="server" Text='<%# Eval("ReturnedAt") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="Edit" NavigateUrl='<%# Eval("Id", Const.Pages.Borrow.Edit) %>' runat="server">Edit</asp:HyperLink> |
                    <asp:HyperLink ID="Details" NavigateUrl='<%# Eval("Id", Const.Pages.Borrow.Details) %>' runat="server">Details</asp:HyperLink> |
                    <asp:HyperLink ID="Delete" NavigateUrl='<%# Eval("Id", Const.Pages.Borrow.Delete) %>' runat="server">Delete</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>