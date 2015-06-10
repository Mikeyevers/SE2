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
            // Administration is de class die o.a. de database connectie bevat,         
            administration = new Administration();
        }

        public Administration administration { get; set; }

        public void LoginBtn_Click(object sender, EventArgs e)
        {
           // Indien emailadres en wachtwoord zijn ingevuld wordt er geprobeerd in te loggen. 
            if (inputEmail.Text != String.Empty && inputPassword.Text != String.Empty)
            {
                if (administration.Inloggen(inputEmail.Text, inputPassword.Text))
                {
                    // Ik heb een methode geschreven die na het inloggen ervoor zorgt dat er naar een bepaalde pagina wordt genavigeerd.
                    // Deze pagina is flexibel en kan door de klant in Web.config worden aangepast. 
                    NavigateAfterLogin(this.Response);
                }
                else
                {
                    // Laat gebruiker zien dat inloggen is mislukt.
                    LoginFailureText.Visible = true;
                }
            }
            else
            {
                // ***Laat gebruiker zien dat de velden niet leeg mogen zijn!
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