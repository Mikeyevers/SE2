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
    }
}