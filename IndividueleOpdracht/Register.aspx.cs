using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IndividueleOpdracht
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Er dient gecontroleerd worden of er is ingelogd.
            // Indien dit het geval is dan dient er naar de (door de klant) ingestelde "pageAfterLogin" (Web.config)
            // genavigeerd te worden.
            if (Session.Count != 0 || Session["UserAuthentication"] != null)
            {
                Master.Administration.NavigateAfterLogin(this.Response);
            }
        }

        protected void btn_annuleren_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void btn_maakAccount_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                string correctEmail = inputEmail.Text.Replace(" ", "");
                correctEmail = correctEmail.ToLower();

                // Controleren of het e-mailadres niet al in gebruik is.
                if(Master.Administration.CheckEmailIsUnique(correctEmail))
                {
                    RegisterFailureText.Text = "<span class=\"text-warning\">Er bestaat al een account met het ingevulde e-mailadres. Kies een ander e-mailadres.</span>";
                    RegisterFailureText.Visible = true;
                }
                else
                {
                    bool emailMarktplaats;
                    bool emailMarktplaatsPartners;

                    if(inputEmailMarktplaats.Checked)
                    {
                        emailMarktplaats = true;
                    }
                    else
                    {
                        emailMarktplaats = false;
                    }

                    if(inputEmailMarktplaatsPartner.Checked)
                    {
                        emailMarktplaatsPartners = true;
                    }
                    else
                    {
                        emailMarktplaatsPartners = false;
                    }


                    bool succes = Master.Administration.CreateAccount(inputName.Text, correctEmail, inputPassword.Text, emailMarktplaats, emailMarktplaatsPartners);
                    if (succes)
                    {
                        bool emailIsSend = Master.Administration.SendActivationMail(correctEmail, inputName.Text);

                        if (emailIsSend == false)
                        {
                            RegisterFailureText.Text = "<span class=\"text-warning\">De bevestigingsmail kon niet worden verstuurd. <br /> Mogelijk heb je een ongeldig e-mailadres opgegeven.</span>";
                            RegisterFailureText.Visible = true;   
                        }
                        else
                        {
                            inputName.Text = String.Empty;
                            inputEmail.Text = String.Empty;
                            inputPassword.Text = String.Empty;
                            inputRepeatPassword.Text = String.Empty;
                            inputEmailMarktplaats.Checked = true;
                            inputEmailMarktplaatsPartner.Checked = false;

                            RegisterFailureText.Text = "<span class=\"text-warning\">We hebben je een bevestigingsmail gestuurd. <br /> Volg de instructies in deze mail om je account te bevestigen.</span>";
                            RegisterFailureText.Visible = true;     
                        }
                             
                    }
                    else
                    {
                        RegisterFailureText.Text = "<span class=\"text-warning\">Account aanmaken mislukt. Controleer je ingevulde gegevens en probeer het opnieuw.</span>";
                         RegisterFailureText.Visible = true;
                    }
                }
            }
            else
            {
                RegisterFailureText.Text = "<span class=\"text-warning\">Account aanmaken mislukt. Controleer je ingevulde gegevens en probeer het opnieuw.</span>";
                RegisterFailureText.Visible = true;
            }
        }
    }
}