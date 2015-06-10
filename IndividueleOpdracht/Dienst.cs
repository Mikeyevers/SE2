using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndividueleOpdracht
{
    public enum AanhefType
    {
        Heer,
        Mevrouw,
        Familie,
        Bedrijf
    };

    public enum AdvertentiePakket
    {
        BasisAdvertentie,
        SuccesAdvertentie,
        RegionaleTopAdvertentie
    };
    public class Dienst : Advertentie
    {
        public string Naam { get; set; }
        public string Slogan { get; set; }
        public AdvertentiePakket AdvertentiePakket { get; set; }
        public string AdvertentieLengte { get; set; }
        public string GekozenRegio { get; set; }
        public int StartjaarWerkzaam { get; set; }
        public string Bedrijfsvorm { get; set; }
        public int AantalMedewerkers { get; set; }
        public int KvkNummer { get; set; }
        public string profielfotoPadVerwijzing { get; set; }
        public AanhefType Aanhef { get; set; }
        public int Huisnummer { get; set; }
        public DateTime TotDatum { get; set; }
        public List<string>  specialiteiten { get; set; }

        public Dienst()
        {

        }
    }
}