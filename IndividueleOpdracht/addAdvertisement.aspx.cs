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
            inputPrijstype.Items.Add("Bieden");     
            inputPrijstype.Items.Add("Gratis");  
            inputPrijstype.Items.Add("Vraag prijs");   
            inputPrijstype.Items.Add("Zie omschrijving");   
            inputPrijstype.Items.Add("Nader overeen te komen");   
            inputPrijstype.Items.Add("ruilen");   
            inputPrijstype.Items.Add("Op aanvraag");   

    
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
    }
}