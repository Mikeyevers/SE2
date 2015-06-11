using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IndividueleOpdracht
{
    public partial class advertentieOverzicht : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Er dient gecontroleerd worden of er is ingelogd.
            // Zo niet dan dient er automatisch naar de inlogpagina genavigeerd te worden.
            if (Session["USER_EMAIL"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}