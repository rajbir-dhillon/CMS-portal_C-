<%@ Page Title="View Customers" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="viewcust.aspx.cs" Inherits="Webnapp.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/addcust.aspx">Add Customer</asp:HyperLink>
    <asp:PlaceHolder id="PlaceHolder1" runat="server"></asp:PlaceHolder>

</asp:Content>
