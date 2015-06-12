<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="ActivateAccount.aspx.cs" Inherits="IndividueleOpdracht.ActivateAccount" %>
<%@ MasterType VirtualPath="~/Masterpage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container" class="jumbotron">
        <asp:Literal ID="activationMessage" runat="server"></asp:Literal>
    </div>
</asp:Content>
