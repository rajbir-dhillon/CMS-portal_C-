<%@ Page Title="Adoptions" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Adoptionsfame.aspx.cs" Inherits="Webnapp.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/halloffame.aspx">View Hall of Fame!</asp:HyperLink></h3>
    <asp:PlaceHolder id="PlaceHolder1" runat="server"></asp:PlaceHolder>
</asp:Content>
