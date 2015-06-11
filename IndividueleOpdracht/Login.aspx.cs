using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IndividueleOpdracht
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Er dient gecontroleerd worden of er is ingelogd.
            // Indien dit het geval is dan dient er naar de (door de klant) ingestelde "pageAfterLogin" (Web.config)
            // genavigeerd te worden.
            if (Session.Count != 0 || Session["UserAuthentication"] != null)
            {
                NavigateAfterLogin(this.Response);
            }

            // Administration is de class die o.a. de database connectie bevat,         
            administration = new Administration();
        }

        public Administration administration { get; set; }

        public void LoginBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (administration.Inloggen(inputEmail.Text, inputPassword.Text))
                {
                    // Session aanmaken voor de ingelogde gebruiker.
                    Session["UserAuthentication"] = inputEmail.Text;

                    // Ik heb een methode geschreven die na het inloggen ervoor zorgt dat er naar een bepaalde pagina wordt genavigeerd.
                    // Deze pagina is flexibel en kan door de klant in Web.config worden aangepast. 
                    NavigateAfterLogin(this.Response);
                }
                else
                {
                    // Laat gebruiker zien dat inloggen is mislukt.
                    LoginFailureText.Text = "<span class=\"text-warning\">Inloggen mislukt. Controleer je wachtwoord en probeer het opnieuw.</span>";
                    LoginFailureText.Visible = true;
                }
            }
            
        }
            
           
        public void NavigateAfterLogin(HttpResponse response)
        {
            // Open van het Web.config bestand.
            Configuration configuration = WebConfigurationManager.OpenWebConfiguration("\\Web.config");

            // Pagina (value) ophalen die door de klant is gekoppeld aan de "pageAfterLogin" (key).
            string pageAfterLogin = configuration.AppSettings.Settings["pageAfterLogin"].Value;

            // Vervolgens navigeren naar de opgehaalde pagina.
            response.Redirect(pageAfterLogin);
        }
    }
}