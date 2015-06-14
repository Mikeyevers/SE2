using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndividueleOpdracht
{

    public class Product : Advertentie
    {
        public string Titel { get; set; }
        public string PrijsType { get; set; }
        public string VraagprijsOptie { get; set; }
        public decimal BiedenVanafBedrag { get; set; }  
        public decimal PrijsBedrag { get; set; } 
        public bool PayPal { get; set; }
        public string Land { get; set; }
        public string Woonplaats { get; set; }
        public bool ToonOpKaart { get; set; }
        public bool TopAdvertentie { get; set; }
        public bool DagTopper { get; set; }
        public DateTime TotDatumTopAdvertentie { get; set; }
        public DateTime TotDatumDagTopper { get; set; }

        public Product(string titel, string prijsType, string vraagprijsOptie, decimal biedenVanafBedrag, decimal prijsBedrag,
                       bool payPal, int rubriekNummer, string advertentieTekst, string websiteUrl, string naamBijAdvertentie, int telefoon, string postcode, DateTime plaatsDatum, int adverteerderNummer)
                       : base(rubriekNummer, advertentieTekst, websiteUrl, naamBijAdvertentie, telefoon, postcode, plaatsDatum, adverteerderNummer)
        {
            this.Titel = titel;
            this.PrijsType = prijsType;
            this.VraagprijsOptie = vraagprijsOptie;
            this.BiedenVanafBedrag = biedenVanafBedrag;
            this.PrijsBedrag = prijsBedrag;
            this.PayPal = payPal;
        }

        public Product(string titel, string prijsType, bool payPal, int rubriekNummer, string advertentieTekst,
                       string websiteUrl, string naamBijAdvertentie, int telefoon, string postcode, DateTime plaatsDatum, int adverteerderNummer)
            : base(rubriekNummer, advertentieTekst, websiteUrl, naamBijAdvertentie, telefoon, postcode, plaatsDatum, adverteerderNummer)
        {
            this.Titel = titel;
            this.PrijsType = prijsType;
            this.VraagprijsOptie = null;
            this.BiedenVanafBedrag = -999m;
            this.PrijsBedrag = -999m;
            this.PayPal = payPal;
        }
    }
}