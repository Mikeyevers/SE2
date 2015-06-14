<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="changeContactData.aspx.cs" Inherits="IndividueleOpdracht.changeContactData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container" class="jumbotron">
        <fieldset>
        <legend>Contactgegevens aanpassen</legend>
        
        <div class="form-group">
            <div class="col-lg-10"> 
                <asp:RequiredFieldValidator ControlToValidate="inputnaam" runat="server" ErrorMessage="Je dient je naam op te geven." Display="Dynamic" CssClass="text-warning"></asp:RequiredFieldValidator>
                <asp:Label CssClass="strongLabel"  runat="server" Text="Naam"></asp:Label>
                <asp:TextBox ID="inputNaam" runat="server" CssClass=" form-control"></asp:TextBox>

                <asp:RegularExpressionValidator ControlToValidate="inputPostcode"  runat="server" ValidationExpression="^[1-9][0-9]{3}\s?[a-zA-Z]{2}$" ErrorMessage="Je hebt geen geldige postcode opgegeven." Display="dynamic" CssClass="text-warning"></asp:RegularExpressionValidator>
                <asp:Label CssClass="strongLabel" runat="server" Text="Postcode"></asp:Label>
                <asp:TextBox ID="inputPostcode" runat="server" CssClass=" form-control"></asp:TextBox>

                <asp:RegularExpressionValidator ControlToValidate="inputTelefoonnummer"  runat="server" ValidationExpression="(^\+[0-9]{2}|^\+[0-9]{2}\(0\)|^\(\+[0-9]{2}\)\(0\)|^00[0-9]{2}|^0)([0-9]{9}$|[0-9\-\s]{10}$)" ErrorMessage="Je hebt geen geldig telefoonnummer opgegeven." Display="dynamic" CssClass="text-warning"></asp:RegularExpressionValidator>
                 <asp:Label CssClass="strongLabel" runat="server" Text="Telefoonnummer"></asp:Label>
                <asp:TextBox ID="inputTelefoonnummer" runat="server" CssClass=" form-control"></asp:TextBox>

                <asp:Label CssClass="strongLabel" runat="server" Text="e-mailadres"></asp:Label>
                <asp:TextBox ID="tbemail" runat="server" CssClass=" form-control" Enabled="false"></asp:TextBox>
            </div>
        </div>

         <div class="form-group">
                <div class="col-lg-10"> 
                    <div class="checkbox">  
                        <label>
                            <asp:CheckBox ID="inputEmailMarktplaats" runat="server" Text="Ja, ik wil op de hoogte blijven van nieuws over Marktplaats, tips voor mijn advertenties en websiteverbeteringen. Ook word ik graag uitgenodigd voor gebruiksonderzoeken." Checked="true"/>
                        </label>
                    </div>
                </div>
         </div>
            <div class="form-group">
                <div class="col-lg-10">   
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox ID="inputEmailMarktplaatsPartner" runat="server" Text="Ja, ik ontvang graag maximaal één aanbieding per maand van zorgvuldig door Marktplaats geselecteerde partners." />
                        </label>
                    </div>

                    <asp:Button ID="btn_gegevensAanpassen" runat="server" Text="Wijzigen" CssClass="btn btn-warning" OnClick="btn_gegevensAanpassen_Click" />
                    <br /><asp:Literal ID="gegevensAanpassenFailureText" runat="server" Visible="false"></asp:Literal>
                </div>
           </div>


           </fieldset>
    </div>
</asp:Content>
