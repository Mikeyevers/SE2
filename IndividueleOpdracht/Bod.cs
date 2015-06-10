using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndividueleOpdracht
{
    public class Bod
    {
        public string BodNummer { get; set; }
        public decimal PrijsBedrag { get; set; }
        public DateTime Datum { get; set; }
        public Adverteerder adverteerder { get; set; }
    }
}