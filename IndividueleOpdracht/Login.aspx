<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="IndividueleOpdracht.Login
    " %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
    <div id="container" class="jumbotron">
        <fieldset>
            <legend>Inloggen</legend>
            <div class="form-group">
                <div class="col-lg-10">       
                    <asp:RequiredFieldValidator Display="Dynamic" ControlToValidate="inputEmail" runat="server" ErrorMessage="Je dient een e-mailadres in te vullen." CssClass="text-warning"></asp:RequiredFieldValidator>      
                    <asp:TextBox runat="server" type="email" Cssclass="form-control" id="inputEmail" placeholder="E-mailadres"></asp:TextBox>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <div class="col-lg-10">     
                    <asp:RequiredFieldValidator Display="Dynamic" ControlToValidate="inputPassword" runat="server" ErrorMessage="Je dient een wachtwoord in te vullen." CssClass="text-warning"></asp:RequiredFieldValidator>               
                    <asp:TextBox runat="server" type="password" Cssclass="form-control" id="inputPassword" placeholder="Wachtwoord" ></asp:TextBox>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group"> 
                <div class="col-lg-10">  
                  <asp:Button ID="btn_inloggen" runat="server" Text="Inloggen" CssClass="btn btn-warning" OnClick="LoginBtn_Click" />
                  <asp:Button ID="btn_maakAccount" runat="server" Text="Account aanmaken" CssClass="btn btn-default" OnClick="btn_maakAccount_Click" CausesValidation="false" />
                </div>
                <div class="col-lg-10">
                     <asp:Literal ID="LoginFailureText" runat="server" Visible ="false"></asp:Literal>                  
                </div>             
            </div>
        </fieldset>
    </div>
</asp:Content>
