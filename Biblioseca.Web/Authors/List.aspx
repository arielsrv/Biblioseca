<%@ Page Title="Authors" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Biblioseca.Web.List" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Index</h2>
    <asp:GridView ID="GridViewAuthors" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
        OnRowDeleting="GridViewAuthors_RowDeleting" OnRowEditing="GridViewAuthors_RowEditing" CssClass="table table-bordered table-hover">
        <Columns>
            <asp:TemplateField HeaderText="First name">
                <ItemTemplate>
                    <asp:Label ID="labelFirstName" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="textFirstName" runat="server" Text='<%# Eval("FirstName") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Last name">
                <ItemTemplate>
                    <asp:Label ID="labelLastName" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="textLastName" runat="server" Text='<%# Eval("LastName") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>
    <asp:LinkButton runat="server" ID="linkCreateNewAuthor" Text="Crear" OnClick="LinkCreateNewAuthor_OnClick"
        CausesValidation="false" CssClass="btn btn-primary"></asp:LinkButton>
</asp:Content>