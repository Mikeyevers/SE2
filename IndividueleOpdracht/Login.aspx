<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="IndividueleOpdracht.Login
    " %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
<form runat="server">
    <div id="container">
        <asp:Literal ID="LoginFailureText" runat="server" Visible ="false">Inloggen mislukt. Controleer je wachtwoord en probeer het opnieuw.</asp:Literal> 
        <fieldset>
            <legend>Inloggen</legend>
            <div class="form-group">
                <div class="col-lg-10">
                    <asp:TextBox runat="server" type="email" Cssclass="form-control" id="inputEmail" placeholder="Email" ></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div class="col-lg-10">                    
                    <asp:TextBox runat="server" type="password" Cssclass="form-control" id="inputPassword" placeholder="Wachtwoord" ></asp:TextBox>
                </div>
            </div>
            <div class="form-group"> 
                <div class="col-lg-10">  
                  <asp:Button ID="LoginBtn" runat="server" Text="Inloggen" CssClass="btn btn-warning" OnClick="LoginBtn_Click" />
                </div>
            </div>
        </fieldset>
    </div>
</form>
</asp:Content>
