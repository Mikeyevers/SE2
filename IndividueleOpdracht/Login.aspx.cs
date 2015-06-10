using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            
        }
    }
}