using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IndividueleOpdracht
{
    public partial class addAdvertisement : System.Web.UI.Page
    {
        Administration administration = new Administration();
        protected void Page_Load(object sender, EventArgs e)
        {
            // Er dient gecontroleerd worden of er is ingelogd.
            // Zo niet dan dient er automatisch naar de inlogpagina genavigeerd te worden.
            if (Session == null || Session["UserAuthentication"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            // Vullen van de hoofdgroepen listbox.
            // I.v.m. Page life-cyclus maak in deze Page_Load een object van Administration aan,
            // i.p.v. gebruik te maken van de instantie in de Masterpage. 
            if (!IsPostBack)
            {
                List<string> groepen = administration.GetMainGroups();

                if (groepen != null)
                {
                    foreach (string groep in groepen)
                    {
                        ListBoxGroepen.Items.Add(groep);
                    }
                } 
            }

            // Vullen van de prijstype dropdown.    
            if (!IsPostBack)
            {
                inputPrijstype.Items.Add("Gratis");
                inputPrijstype.Items.Add("Bieden");
                inputPrijstype.Items.Add("Vraag prijs");
                inputPrijstype.Items.Add("Zie omschrijving");
                inputPrijstype.Items.Add("Nader overeen te komen");
                inputPrijstype.Items.Add("ruilen");
                inputPrijstype.Items.Add("Op aanvraag"); 
            }  

            // Enter key instellen.
            this.Form.DefaultButton = btn_plaatsAdvertentie.UniqueID;
        }

        protected void ListBoxGroepen_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Listboxen voorrubrieken en subgroepen dienen leeg gemaakt te worden.
            ListBoxRubrieken.Items.Clear();
            ListBoxSubgroepen.Items.Clear();

            // Vullen van de subgroepen listbox (Voorlopig gekozen voor een postback).
            string mainGroup = ListBoxGroepen.SelectedValue;
            List<string> subGroepen = administration.GetSubGroups(mainGroup);

            if (subGroepen != null)
            {
                foreach (string subgroep in subGroepen)
                {
                    ListBoxSubgroepen.Items.Add(subgroep);
                }
            }
        }

        protected void ListBoxSubgroepen_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Vullen van de rubrieken listbox (Voorlopig gekozen voor een postback).
            string subGroup = ListBoxSubgroepen.SelectedValue;
            List<string> rubrieken = administration.GetRubrics(subGroup);

            if (rubrieken != null)
            {
                foreach (string rubriek in rubrieken)
                {
                    ListBoxRubrieken.Items.Add(rubriek);
                }
            }
        }

        protected void inputPrijstype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(inputPrijstype.SelectedValue == "Vraag prijs")
            {
                inputVraagprijsREValidator.Enabled = true;
                inputVraagprijsRFValidator.Enabled = true;
                inputVraagprijs.Visible = true;
                RadioButtonListVraagprijs.Visible = true;    
            }
        }

        protected void RadioButtonListVraagprijs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonListVraagprijs.SelectedValue == "start bieden vanaf")
            {
                inputStartBiedenVanaf.Visible = true;
                RegularExpressionValidatorSBV.Enabled = true;
                RequiredFieldValidatorSBV.Enabled = true;
            }
        }

        protected void btn_plaatsAdvertentie_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                // Eerst wil ik weten wie de advertentie maakt door een Adverteerder object te maken.
                string email = Session["UserAuthentication"].ToString();
                Adverteerder user = Master.Administration.getUserByEmail(email);
                
                string localVraagprijs = inputVraagprijs.Text;
                if(localVraagprijs == "")
                {
                    localVraagprijs = null;
                }

                string localVraagprijsOptie = RadioButtonListVraagprijs.SelectedValue;
                if(localVraagprijsOptie == "")
                {
                    localVraagprijsOptie = null;
                }

                string localBiedenVanafBedrag =  inputStartBiedenVanaf.Text;
                if(localBiedenVanafBedrag == "")
                {
                    localBiedenVanafBedrag = null;
                }

                string localPrijsbedrag = inputVraagprijs.Text;
                if(localPrijsbedrag == "")
                {
                    localPrijsbedrag = null;
                }

                string boolPaypal = "nee";
                if(inputPaypal.Checked)
                {
                    boolPaypal = "ja";
                }

                string localWebsiteUrl = inputWebsite.Text.ToLower(); 
                if(localWebsiteUrl == "")
                {
                    localWebsiteUrl = null;
                }

                
                
                int rubriekNummer = Master.Administration.GetRubricNumber(ListBoxRubrieken.SelectedValue);
                string txt = inputTekst.Text;
                string[] lines = txt.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                string txtWithSpaces = "";
                for (int i = 0; i < lines.Length; i++)
                {
                    txtWithSpaces += lines[i] + " ";
                }
                
                // Daarna gaan we de advertentie plaatsen.
                bool succes = user.PlaceAdvertisement(inputTitel.Text, inputPrijstype.SelectedValue.ToLower(), localVraagprijsOptie, localBiedenVanafBedrag, localPrijsbedrag,
                                        boolPaypal, rubriekNummer, txtWithSpaces, localWebsiteUrl, user.Naam, user.Telefoonnummer, user.AdverteerderNummer, user.Postcode, null, null);
                if (succes)
                {
                    Response.Redirect("advertisementSummary.aspx");
                }
                else
                {
                    addingFailureText.Text = "<span class=\"text-warning\">Er is iets misgegaan probeer het opnieuw.</span>";
                    addingFailureText.Visible = true;
                }      
            }
            else
            {
                // Laat de gebruiker zien dat er niet alle data goed is ingevuld
                addingFailureText.Text = "<span class=\"text-warning\">Advertentie toevoegen mislukt. Controleer je ingevulde gegevens en probeer het opnieuw.</span>";
                addingFailureText.Visible = true;
            }
           
        }

        protected void btn_annuleren_Click(object sender, EventArgs e)
        {
            Response.Redirect("advertisementSummary.aspx");
        }
    }
}