using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace IndividueleOpdracht
{
    public class Advertentie
    {
        public int AdverteerderNummer { get; set; }
        public int RubriekNummer { get; set; }
        public string AdvertentieTekst { get; set; }
        public string WebsiteUrl { get; set; }
        public string NaamBijAdvertentie { get; set; }
        public int TelefoonBijAdvertentie { get; set; }
        public string Postcode { get; set; }
        public DateTime PlaatsDatum { get; set; }


        public Advertentie(int rubriekNummer, string advertentieTekst, string websiteUrl, string naamBijAdvertentie, int telefoonBijAdvertentie, string postcode, DateTime plaatsDatum, int adverteerderNummer)
        {
            this.RubriekNummer = rubriekNummer;
            this.AdvertentieTekst = advertentieTekst;
            this.WebsiteUrl = websiteUrl;
            this.NaamBijAdvertentie = naamBijAdvertentie;
            this.TelefoonBijAdvertentie = telefoonBijAdvertentie;
            this.Postcode = postcode;
            this.PlaatsDatum = plaatsDatum;
            this.AdverteerderNummer = adverteerderNummer;
        }
    }
}