using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IndividueleOpdracht
{
    public partial class ActivateAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Controleren of de ActivationCode in de URL bestaat. Zo ja dan wordt het bijbehorende record verwijderd. 
            // Not niet geïmplementeerd: beveiliging voor onjuiste parameters.
            if (!this.IsPostBack)
            {
                string activationCode = !string.IsNullOrEmpty(Request.QueryString["ActivationCode"]) ? Request.QueryString["ActivationCode"] : Guid.Empty.ToString();

                string returnedActivationMessage = Master.Administration.DeleteActivationCode(activationCode);

                activationMessage.Text = "<span class=\"text-warning\">" + returnedActivationMessage + "</span>";    
            }
        }
    }
}