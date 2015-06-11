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
        Administration administration;
        protected void Page_Load(object sender, EventArgs e)
        {
            administration = new Administration();

            // Er dient gecontroleerd worden of er is ingelogd.
            // Indien dit het geval is dan dient er naar de (door de klant) ingestelde "pageAfterLogin" (Web.config)
            // genavigeerd te worden.
            if (Session.Count != 0 || Session["UserAuthentication"] != null)
            {
                administration.NavigateAfterLogin(this.Response);
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
                // Controleren of het e-mailadres niet al in gebruik is.
                if(administration.checkEmailIsUnique(inputEmail.Text))
                {
                    RegisterFailureText.Text = "<span class=\"text-warning\">Email is uniek jonguh!.</span>";
                    RegisterFailureText.Visible = true;
                }
                else
                {
                    RegisterFailureText.Text = "<span class=\"text-warning\">Er bestaat al een account met het ingevulde e-mailadres. Kies een ander e-mailadres.</span>";
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
}