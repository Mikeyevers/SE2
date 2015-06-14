using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IndividueleOpdracht
{
    public partial class advertentieUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private Product product; 
        public Product Product
        { 
            get { return product;}
            set
            {
                product = value;
                string plaatsDatum = product.PlaatsDatum.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                
                advertisementTitle.Text = "<span class=\"panel-title\">" + product.Titel + "</span>" + "<span style=\"float: right;\">" + plaatsDatum + "</span>";
                naam.Text = "<strong>Adverteerder: </strong>" + product.NaamBijAdvertentie;
                rubriekNaam.Text = "<strong>Rubriek: </strong>" + product.getRubricNameByNumber(product.RubriekNummer);
                content.Text = product.AdvertentieTekst;
                website.Text = product.WebsiteUrl;
                prijsType.Text = "<strong>Prijs type: </strong>" + product.PrijsType;
                if (product.PrijsBedrag != -999M)
                {
                    Vraagprijs.Text = "<strong>Prijs: </strong>" + product.PrijsBedrag;
                    Vraagprijs.Visible = true;
                }
                if (product.BiedenVanafBedrag != -999M)
                {
                    biedenVanafBedrag.Text = "<strong>U kunt bieden vanaf</strong> € " + product.BiedenVanafBedrag;
                    biedenVanafBedrag.Visible = true;
                }
                if (product.TelefoonBijAdvertentie != -999)
                {
                    telefoon.Text = "<strong>Telefoon: </strong>" + product.TelefoonBijAdvertentie;
                }
                postcode.Text = "<strong>Postcode: </strong>" + product.Postcode;
                woonplaats.Text = "<strong>Woonplaats: </strong>" + product.Woonplaats;
                land.Text = "<strong>Land: </strong>" + product.Land;

                if (product.PayPal)
                {
                    payPal.Visible = true;
                }
                
            } 
        }
    }
}