using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace IndividueleOpdracht
{
    public class Advertentie
    {
        public string AdvertentieNummer { get; set; }
        public string RubriekNummer { get; set; }
        public string GroepNummer { get; set; }
        public string SubgroepNummer { get; set; }
        public string AdvertetieTekst { get; set; }
        public string WebsiteUrl { get; set; }
        public string NaamBijAdvertentie { get; set; }
        public string TelefoonBijAdvertentie { get; set; }
        public string Postcode { get; set; }
        public string AantalKeerGezien { get; set; }
        public DateTime PlaatsDatum { get; set; }
        public string Link { get; set; }
        public List<Image> Fotos { get; set; }
    }
}