using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IndividueleOpdracht
{
    public partial class changeContactData : System.Web.UI.Page
    {
        // I.v.m. de page life-cyclus maak ik hier een nieuwe instantie van Administration.
        // I.p.v. gebruik te maken van het MasterType.
        Administration administration = new Administration();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Er dient gecontroleerd worden of er is ingelogd.
            // Zo niet dan dient er automatisch naar de inlogpagina genavigeerd te worden.
            if (Session == null || Session["UserAuthentication"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            // Huidige contactgegevens data ophalen en laden op het scherm.
            if (!IsPostBack)
            {
                tbemail.Text = Session["UserAuthentication"].ToString();

                Adverteerder currentAdverteerder = administration.getUserByEmail(tbemail.Text);
                inputNaam.Text = currentAdverteerder.Naam;
                inputPostcode.Text = currentAdverteerder.Postcode;
                inputTelefoonnummer.Text = currentAdverteerder.Telefoonnummer;
                inputEmailMarktplaats.Checked = currentAdverteerder.EmailMarktplaats;
                inputEmailMarktplaatsPartner.Checked = currentAdverteerder.EmailMarktplaatsPartners;
            }

            // Enter key instellen.
            this.Form.DefaultButton = btn_gegevensAanpassen.UniqueID;

        }

        protected void btn_gegevensAanpassen_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //Object maken van current user.
                string email = Session["UserAuthentication"].ToString();
                Adverteerder currentAdverteerder = administration.getUserByEmail(email);

                string name = inputNaam.Text.Trim();
                string zipCode = inputPostcode.Text.Replace(" ", "");
                string phoneNumber = inputTelefoonnummer.Text;
                string emailMarktplaats = "nee";
                string emailMarktplaatsPartners = "nee";
                if (inputEmailMarktplaats.Checked)
                {
                    emailMarktplaats =  "ja";
                }
                if (inputEmailMarktplaatsPartner.Checked)
                {
                    emailMarktplaatsPartners = "ja";
                }

                //Updaten en op de hoogte brengen van de gebruiker.
                bool succes = currentAdverteerder.changeContactDate(name, zipCode, phoneNumber, emailMarktplaats, emailMarktplaatsPartners);

                if (succes)
                {
                    gegevensAanpassenFailureText.Text = "<span class=\"text-warning\">De wijzigingen zijn opgeslagen</span>";
                    gegevensAanpassenFailureText.Visible = true;
                }
                else
                {
                    gegevensAanpassenFailureText.Text = "<span class=\"text-warning\">Er is iets fout gegaan probeer het opnieuw.</span>";
                    gegevensAanpassenFailureText.Visible = true;
                }
            }
            else
            {
                gegevensAanpassenFailureText.Text = "<span class=\"text-warning\">Niet alle data in correct ingevuld. Probeer het opnieuw.</span>";
                gegevensAanpassenFailureText.Visible = true;
            }
        }
    }
}