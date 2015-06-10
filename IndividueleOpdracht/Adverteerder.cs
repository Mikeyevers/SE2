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
    }
}