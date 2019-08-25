<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="adopt.aspx.cs" Inherits="Webnapp.adopt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <div class="form-group">
            <asp:PlaceHolder id="PlaceHolder1" runat="server"></asp:PlaceHolder>
              Customer ID:<br />
              <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
              <br />
              Donation amount:<br />
              <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <br />
            Currency<br />
            <asp:DropDownList ID="DropDownList1" runat="server" >
                <asp:ListItem Value="1">Pound</asp:ListItem>
                <asp:ListItem Value="2">Euros</asp:ListItem>
                <asp:ListItem Value="3">Lei</asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        </div>
    </div>
    
    
    
</asp:Content>
