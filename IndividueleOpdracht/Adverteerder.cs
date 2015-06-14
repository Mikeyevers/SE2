using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndividueleOpdracht
{
    public class Adverteerder
    {
        public int AdverteerderNummer { get; set; }
        public string Emailadres { get; set; }
        public string Naam { get; set; }
        public string Postcode { get; set; }
        public string Telefoonnummer { get; set; }
        public bool EmailMarktplaats { get; set; }
        public bool EmailMarktplaatsPartners { get; set; }

        public Administration administration = new Administration();

        public Adverteerder(int adverteerderNummer, string emailadres, string naam, string postcode,  string telefoonnummer, 
                            bool emailMarktplaats, bool emailMarktplaatsPartners)
        {
            this.AdverteerderNummer = adverteerderNummer;
            this.Emailadres = emailadres;
            this.Naam = naam;
            this.Postcode = postcode;
            this.Telefoonnummer = telefoonnummer;
            this.EmailMarktplaats = emailMarktplaats;
            this.EmailMarktplaatsPartners = emailMarktplaatsPartners;
        }

        public bool PlaceAdvertisement(string titel, string selectedPrijstype, string selectedVraagprijsOptie, string biedenVanafBedrag, string prijsBedrag, 
                                       string payPal, int rubriekNummer, string advertentieTekst, string websiteUrl, string naamBijAdvertentie, string telefoon, int adverteerderNummer, string postcode, string land, string woonplaats)
        {
            // Eerst een advertentie nummer ophalen.
            string query1 = "INSERT INTO advertentie VALUES(ADVERTENTIENUMMER_SEQ.nextval, :RUBRIEKNUMMER, :ADVERTEERDERNUMMER, :ADVERTENTIETEKST, :WEBSITEURL, :NAAMBIJADVERTENTIE, :TELEFOON, :POSTCODE, null, null, default, sysdate, null)";
            OracleParameter rubriekNummerParameter = new OracleParameter(":RUBRIEKNUMMER", rubriekNummer);
            OracleParameter adverteerderNummerParameter = new OracleParameter(":ADVERTEERDERNUMMER", adverteerderNummer);
            OracleParameter advertentieTekstParameter = new OracleParameter(":ADVERTENTIETEKST", advertentieTekst);
            OracleParameter websiteUrlParameter = new OracleParameter("WEBSITEURL", websiteUrl);
            OracleParameter naamBijAdvertentieParameter = new OracleParameter(":NAAMBIJADVERTENTIE", naamBijAdvertentie);
            OracleParameter telefoonParameter = new OracleParameter(":TELEFOON", telefoon);
            OracleParameter postcodeParameter = new OracleParameter("POSTCODE", postcode);
            OracleParameter[] parameters1 = new OracleParameter[] {rubriekNummerParameter, adverteerderNummerParameter,
                                                                   advertentieTekstParameter, websiteUrlParameter,
                                                                   naamBijAdvertentieParameter, telefoonParameter, postcodeParameter};

            int rowCount1 = administration.DatabaseConnection.ExecuteNonQuery(query1, parameters1);

            if (rowCount1 == -1)
            {
                return false;
            }

            // Daarna de product (en dienst in toekomst) tabel vullen.
            string query2 = "INSERT INTO product VALUES (ADVERTENTIENUMMER_SEQ.currval, :TITEL, :PRIJSTYPE, :VRAAGPRIJSOPTIE, :BIEDENVANAFBEDRAG, :PRIJSBEDRAG, :PAYPAL, :LAND, :WOONPLAATS, null, null, null, null, null)";
            OracleParameter titelParameter = new OracleParameter(":TITEL", titel);
            OracleParameter selectedPrijstypeParameter = new OracleParameter(":PRIJSTYPE", selectedPrijstype);
            OracleParameter selectedVraagprijsOptieParameter = new OracleParameter(":VRAAGPRIJSOPTIE", selectedVraagprijsOptie);
            OracleParameter biedenVanafBedragParameter = new OracleParameter(":BIEDENVANAFBEDRAG", biedenVanafBedrag);
            OracleParameter prijsBedragParameter = new OracleParameter(":PRIJSBEDRAG", prijsBedrag);
            OracleParameter payPalParameter = new OracleParameter(":PAYPAL", payPal);
            OracleParameter landParameter = new OracleParameter(":LAND", land);
            OracleParameter woonplaatsParameter = new OracleParameter(":WOONPLAATS", woonplaats);
            OracleParameter[] parameters2 = new OracleParameter[] {titelParameter, selectedPrijstypeParameter,
                                                                  selectedVraagprijsOptieParameter, biedenVanafBedragParameter, prijsBedragParameter, 
                                                                  payPalParameter, landParameter, woonplaatsParameter};

            int rowCount2 = administration.DatabaseConnection.ExecuteNonQuery(query2, parameters2);

            if (rowCount2 == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}