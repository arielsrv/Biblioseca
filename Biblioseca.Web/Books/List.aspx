<%@ Page Title="Books" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Biblioseca.Web.Books.List" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Index</h2>
    <asp:GridView ID="GridViewBooks" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
        OnRowDeleting="GridViewBooks_RowDeleting" OnRowEditing="GridViewBooks_RowEditing" CssClass="table table-bordered table-hover">
        <Columns>
            <asp:TemplateField HeaderText="Title">
                <ItemTemplate>
                    <asp:Label ID="labelTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="labelTitle" runat="server" Text='<%# Eval("Title") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>            
            <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>
    <asp:LinkButton runat="server" ID="linkCreateBook" Text="Crear" OnClick="LinkCreateBook_OnClick"
        CausesValidation="false" CssClass="btn btn-primary"></asp:LinkButton>
</asp:Content>
