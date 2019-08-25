<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="addcust.aspx.cs" Inherits="Webnapp.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
          <div class="form-group">
          
              Name<br />
              <asp:TextBox ID="name" runat="server"></asp:TextBox>
              <br />
              Email<br />
              <asp:TextBox ID="email" runat="server"></asp:TextBox>
              <br />
              Address<br />
              <asp:TextBox ID="address" runat="server"></asp:TextBox>
              <br />
              Country<br />
              <asp:DropDownList ID="countryList" runat="server">
                  <asp:ListItem Value="1">Uk</asp:ListItem>
                  <asp:ListItem Value="2">Germany</asp:ListItem>
                  <asp:ListItem Value="3">Romania</asp:ListItem>
              </asp:DropDownList>
              <br />
              Contact Number<br />
              <asp:TextBox ID="tel_no" runat="server"></asp:TextBox>
              <br />
              Picture<asp:FileUpload ID="FileUpload1" runat="server" />
              <br />
              <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
          </div>
    </div>
</asp:Content>
