using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IndividueleOpdracht
{
    public partial class Masterpage : System.Web.UI.MasterPage
    {
        public Administration Administration { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Administration = new Administration();
        }

        
        protected void Uitloggen_Click(object sender, EventArgs e)
        {
            // Controleren of de session bestaat en indien hij bestaat wordt hij verwijderd.
            if (Session != null || Session["UserAuthentication"] != null)
            {
                Session.Remove("UserAuthentication");
            }

            // Na het uitloggen dient er genavigeerd te worden naar de Login pagina.
            Response.Redirect("Login.aspx");
        }
    }
}