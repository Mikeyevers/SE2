<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="advertentieUserControl.ascx.cs" Inherits="IndividueleOpdracht.advertentieUserControl" %>

<div class="panel panel-warning">
    <div class="panel-heading">
        <asp:Literal ID="advertisementTitle" runat="server" CssClass="panel-title"></asp:Literal>
    </div>
    <div class="panel-body">
        <asp:Label ID="content" runat="server" Text="Label"></asp:Label> 
    </div>
</div>