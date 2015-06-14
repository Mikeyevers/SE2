using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IndividueleOpdracht
{
    public partial class advertisementSummary : System.Web.UI.Page
    {
        private Control advertisementControl;

        // I.v.m. de page life-cyclus dien ik hier een nieuwe instantie van Administration te maken. 
        // I.p.v. gebruik te maken van een MasterType.
        Administration administration = new Administration();
        protected void Page_Load(object sender, EventArgs e)
        {
            // Er dient gecontroleerd worden of er is ingelogd.
            // Zo niet dan dient er automatisch naar de inlogpagina genavigeerd te worden.
            if (Session == null || Session["UserAuthentication"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            
            // Toevoegen van de advertenties aan de pagina.
            List<Product> producten = administration.GetAllProductAdvertisements();
            foreach (Product product in producten)
            {
                addAdvertisementToUI(product);
            }
        }


        private void addAdvertisementToUI(Product product)
        {
            advertisementControl = LoadControl("~/advertentieUserControl.ascx");
            advertentieUserControl AdvertisementUC = (advertentieUserControl)advertisementControl;

            AdvertisementUC.Product = product;
            advertisementsPanel.Controls.Add(AdvertisementUC);
        }
    }
}