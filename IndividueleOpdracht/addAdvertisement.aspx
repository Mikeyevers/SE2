<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="addAdvertisement.aspx.cs" Inherits="IndividueleOpdracht.addAdvertisement" %>
<%@ MasterType VirtualPath="~/Masterpage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container" class=" jumbotron">
        <fieldset>
            <legend>Advertentie plaatsen</legend>

                    <h5><strong>Kies een groep, subgroep en rubriek waarin u uw advertentie wilt plaatsen.</strong></h5>
                    <asp:ListBox ID="ListBoxGroepen" runat="server" OnSelectedIndexChanged="ListBoxGroepen_SelectedIndexChanged" AutoPostBack="true" style="width: 250px;"></asp:ListBox>    
                    <asp:ListBox ID="ListBoxSubgroepen" runat="server" OnSelectedIndexChanged="ListBoxSubgroepen_SelectedIndexChanged" AutoPostBack="true" style="width: 250px;"></asp:ListBox>   
                    <asp:ListBox ID="ListBoxRubrieken" runat="server" style="width: 250px;"></asp:ListBox>       
                   
                    <div class="form-group">
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Je dient een titel in te vullen." ControlToValidate="inputTitel" CssClass="text-warning" Display="Dynamic"></asp:RequiredFieldValidator>
                        <br />
                        <label class="control-label" for="inputTitel">Titel</label>
                        <asp:TextBox ID="inputTitel" Cssclass="form-control" runat="server" placeholder="verplicht"></asp:TextBox>
                    </div>   
            
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="Je dient een advertentie tekst in te vullen." ControlToValidate="inputTekst" CssClass="text-warning" Display="Dynamic"></asp:RequiredFieldValidator>
                    <div class="form-group">
                        <label class="control-label" for="inputTekst">Schrijf een advertentie tekst</label>
                        <asp:TextBox id="inputTekst" placeholder="verplicht" TextMode="multiline" Columns="50" Rows="5" runat="server" CssClass=" form-control"/>
                    </div> 
            
                    <div class="form-group">    
                        <label class="control-label" for="inputTitel">Website</label>
                        <asp:TextBox ID="inputWebsite" Cssclass="form-control" runat="server"></asp:TextBox>
                    </div>    

                    <div class="form-group">    
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Je dient een prijstype te kiezen." ControlToValidate="inputPrijstype" CssClass="text-warning" Display="Dynamic"></asp:RequiredFieldValidator>
                        <label class="control-label" for="inputPrijstype">Kies een prijstype (verplicht)</label>
                        <asp:DropDownList ID="inputPrijstype" runat="server" CssClass="form-control" OnSelectedIndexChanged="inputPrijstype_SelectedIndexChanged" AutoPostBack ="true"></asp:DropDownList>
                       
                        
                        <asp:RequiredFieldValidator id="inputVraagprijsRFValidator" runat="server" ErrorMessage="Je dient een vraagprijs in te vullen." Enabled="false" ControlToValidate="inputVraagprijs" CssClass="text-warning" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="inputVraagprijsREValidator" runat="server" ControlToValidate="inputVraagprijs" Enabled="false" ValidationExpression="^\d{0,8}(\.\d{1,4})?$"  ErrorMessage="Je hebt een ongeldig bedrag opgegeven." CssClass="text-warning" Display="Dynamic"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="inputVraagprijs" runat="server" CssClass="form-control" Visible="false" placeholder="0,00"></asp:TextBox>
                        <br />
                        <asp:RadioButtonList ID="RadioButtonListVraagprijs" runat="server" Visible="false" OnSelectedIndexChanged="RadioButtonListVraagprijs_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem value="VastePrijs">Vaste prijs</asp:ListItem>
                            <asp:ListItem value="VrijBiedenToegestaan">Vrij bieden toegestaan</asp:ListItem>
                            <asp:ListItem value="StartBiedenVanaf">Start bieden vanaf:</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator id="RequiredFieldValidatorSBV" runat="server" ControlToValidate="inputStartBiedenVanaf" ErrorMessage ="Je dient een bieden-vanaf-prijs in te vullen." Enabled="false"  CssClass="text-warning" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorSBV" runat="server" ControlToValidate="inputStartBiedenVanaf" Enabled="false" ValidationExpression="^\d{0,8}(\.\d{1,4})?$"  ErrorMessage="Je hebt een ongeldig bedrag opgegeven." CssClass="text-warning" Display="Dynamic"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="inputStartBiedenVanaf" runat="server" CssClass="form-control" Visible="false" placeholder ="0,00"></asp:TextBox>
                    

                        <div class="checkbox">
                        <label>
                            <asp:CheckBox Checked="false" ID="inputPaypal" runat="server" Text="Accepteer bank- of creditcard-betalingen via PayPal en bied de zekerheid van PayPal-Aankoopbescherming." />
                        </label>
                        </div>
                    </div>    
                    <div class="form-group">                        
                        <asp:Button ID="btn_plaatsAdvertentie" runat="server" Text="Plaats advertentie" CssClass="btn btn-warning" OnClick="btn_plaatsAdvertentie_Click" />
                        <asp:Button ID="btn_annuleren" runat="server" Text="Annuleren" CssClass="btn btn-default" OnClick="btn_annuleren_Click" CausesValidation="false" />
                    </div>  
                    <div class="col-lg-10">
                     <asp:Literal ID="addingFailureText" runat="server" Visible ="false"></asp:Literal>                  
                    </div>   
                                         
        </fieldset>
    </div>
</asp:Content>
