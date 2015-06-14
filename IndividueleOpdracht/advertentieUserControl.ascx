<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="advertentieUserControl.ascx.cs" Inherits="IndividueleOpdracht.advertentieUserControl" %>

<div class="panel panel-warning" id="advertisementUC">
    <div class="panel-heading">
        <asp:Literal ID="advertisementTitle" runat="server"></asp:Literal>
    </div>
    <div class="panel-body">
        <asp:Label ID="naam" runat="server" ></asp:Label><br /><br />
        <asp:Label ID="rubriekNaam" runat="server" ></asp:Label><br /><br />
        <asp:Label ID="content" runat="server" ></asp:Label><br /><br />
        <asp:Label ID="prijsType" runat="server"></asp:Label><br /><br />
        <asp:Label ID="Vraagprijs" runat="server" Visible="false"></asp:Label><br /><br />
        <asp:Label ID="biedenVanafBedrag" runat="server" Visible="false"></asp:Label><br /><br />
        <asp:Label ID="payPal" runat="server" Text="U kunt bij deze advertentie met paypal betalen!" Visible="false"></asp:Label><br /><br />
        <asp:Label ID="website" runat="server"></asp:Label><br /><br />

        <asp:Label ID="telefoon" runat="server"></asp:Label><br /><br />
        <asp:Label ID="postcode" runat="server"></asp:Label><br /><br />
        <asp:Label ID="woonplaats" runat="server"></asp:Label><br /><br />
        <asp:Label ID="land" runat="server"></asp:Label><br /><br />
    </div>
</div>