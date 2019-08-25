<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Webnapp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Pet Adoption</h1>
        <p class="lead">Search through adverts for Pets for adoption in UK, Romania, and Germany, from our own rescue centres.</p>
          <div class="form-group">
                <label for="exampleInputPassword1">Pet Type</label>
                <asp:DropDownList class="form-control form-control-lg" ID="ListPet" runat="server" OnSelectedIndexChanged="ListPet_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
          </div>
          <div class="form-group">
                <label for="exampleInputPassword1">Pet Breed</label>
                <asp:DropDownList class="form-control form-control-lg" ID="Listbreed" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
          </div>
          <div class="form-group">
                <label for="exampleInputPassword1">Sanctuary</label>
                <asp:DropDownList class="form-control form-control-lg" ID="ListSanc" runat="server"  AutoPostBack="True">
                    <asp:ListItem Value="1">UK</asp:ListItem>
                    <asp:ListItem Value="2">Germany</asp:ListItem>
                    <asp:ListItem Value="3">Romania</asp:ListItem>
                </asp:DropDownList>
                
          </div>
            <asp:Button class="btn btn-primary" ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
            <br />
            <asp:Button class="btn btn-primary" ID="Button2" runat="server" OnClick="Button2_Click" Text="View All!" />

        
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:PlaceHolder id="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </div>
        
    </div>

</asp:Content>
