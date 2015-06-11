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
                    <asp:RequiredFieldValidator ControlToValidate="inputEmail" runat="server" ErrorMessage="Je dient een e-mailadres in te vullen." CssClass="text-warning"></asp:RequiredFieldValidator>      
                    <asp:TextBox runat="server" type="email" Cssclass="form-control" id="inputEmail" placeholder="Email" ></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div class="col-lg-10">     
                    <asp:RequiredFieldValidator ControlToValidate="inputPassword" runat="server" ErrorMessage="Je dient een wachtwoord in te vullen" CssClass="text-warning"></asp:RequiredFieldValidator>               
                    <asp:TextBox runat="server" type="password" Cssclass="form-control" id="inputPassword" placeholder="Wachtwoord" ></asp:TextBox>
                </div>
            </div>
            <div class="form-group"> 
                <div class="col-lg-10">  
                  <asp:Button ID="LoginBtn" runat="server" Text="Inloggen" CssClass="btn btn-warning" OnClick="LoginBtn_Click" />
                </div>
                <div class="col-lg-10">
                     <asp:Literal ID="LoginFailureText" runat="server" Visible ="false"></asp:Literal>                  
                </div>             
            </div>
        </fieldset>
    </div>
</asp:Content>
