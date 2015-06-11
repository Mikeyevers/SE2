<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="IndividueleOpdracht.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container" class="jumbotron">
        <fieldset>
            <legend>Account aanmaken</legend>
            <div class="form-group">
                <div class="col-lg-10">   
                    <asp:RequiredFieldValidator ControlToValidate="inputName" runat="server" ErrorMessage="Je dient een naam in te vullen." CssClass="text-warning"></asp:RequiredFieldValidator>   
                    <asp:TextBox runat="server" type="text" Cssclass="form-control" id="inputName" placeholder="Uw naam op Marktplaats"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div class="col-lg-10">   
                    <asp:RequiredFieldValidator ControlToValidate="inputEmail" runat="server" ErrorMessage="Je dient een e-mailadres in te vullen." CssClass="text-warning"></asp:RequiredFieldValidator>   
                    <asp:TextBox runat="server" type="email" Cssclass="form-control" id="inputEmail" placeholder="E-mailadres"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div class="col-lg-10">   
                    <asp:RequiredFieldValidator ControlToValidate="inputPassword" runat="server" ErrorMessage="Je dient een wachtwoord in te vullen." CssClass="text-warning"></asp:RequiredFieldValidator>   
                    <asp:TextBox runat="server" type="password" Cssclass="form-control" id="inputPassword" placeholder="Wachtwoord"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div class="col-lg-10">   
                    <asp:RequiredFieldValidator ControlToValidate="inputRepeatPassword" runat="server" ErrorMessage="Je dient je wachtwoord nog een keer in te vullen." CssClass="text-warning"></asp:RequiredFieldValidator>   
                    <asp:TextBox runat="server" type="password" Cssclass="form-control" id="inputRepeatPassword" placeholder="Herhaal Wachtwoord"></asp:TextBox>
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
                </div>
            </div>
            <div class="form-group">
                <div class="col-lg-10"> 
                    <asp:Button ID="btn_maakAccount" runat="server" Text="Maak accountaan" CssClass="btn btn-warning" />
                    <asp:Button ID="btn_annuleren" runat="server" Text="Annuleren" CssClass="btn btn-default" OnClick="btn_annuleren_Click" CausesValidation="false"/>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>
