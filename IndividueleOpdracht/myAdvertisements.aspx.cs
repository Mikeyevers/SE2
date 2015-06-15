using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IndividueleOpdracht
{
    public partial class myAdvertisements : System.Web.UI.Page
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

            // Object aanmaken van currentUser, wordt altijd gevuld indien er is ingelogd. 
            // Indien er geen currentAdverteerder is (niet ingelogd) dan wordt hierboven een redirect uitgevoerd.
            Adverteerder currentAdverteerder = administration.GetUserByEmail(Session["UserAuthentication"].ToString());

            // Toevoegen van de advertenties aan de pagina.
            List<Product> producten =
                administration.GetCurrentUserProductAdvertisements(currentAdverteerder.AdverteerderNummer);
            foreach (Product product in producten)
            {
                addAdvertisementToUI(product);
            }
        }

        //Voldoet momenteel niet aan dont repeat yourself. Wordt niet meer nagekeken door docent vandaar.
        private void addAdvertisementToUI(Product product)
        {
            advertisementControl = LoadControl("~/advertentieUserControl.ascx");
            advertentieUserControl AdvertisementUC = (advertentieUserControl)advertisementControl;

            AdvertisementUC.Product = product;
            myadvertisementsPanel.Controls.Add(AdvertisementUC);
        }
    }
}