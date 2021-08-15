<%@ Page Title="Create book" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="Biblioseca.Web.Books.Create" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Create</h2>
    <div class="form-horizontal">
        <h4>Book</h4>
        <hr />
        <div class="form-group">
            <div class="col-md-10">
                <asp:TextBox ID="textBoxTitle" placeholder="Title" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="textBoxFirstNameRequiredFieldValidator" runat="server"
                    ErrorMessage="El titulo es obligatorio" ControlToValidate="textBoxTitle" ForeColor="Red">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-10">
                <asp:DropDownList ID="authorList" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-10">
                <asp:Button ID="buttonCreateBook" runat="server" Text="Crear" OnClick="ButtonCreateAuthor_Click" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>