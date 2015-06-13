﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndividueleOpdracht
{
    public enum PrijsType 
    {
        VraagPrijs,
        Bieden,
        NaderOvereenTeKomen,
        OpAanvraag,
        ZieOmschrijving,
        Ruilen,
        Gratis
    };

    public enum VraagprijsOptie
    {
        VastePrijs,
        VrijBiedenToestaan,
        StartBiedenVanaf
    };
    public class Product : Advertentie
    {
        public string Titel { get; set; }
        public PrijsType PrijsType { get; set; }
        public VraagprijsOptie VraagprijsOptie { get; set; }
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

        public Product(string titel, PrijsType prijsType, VraagprijsOptie vraagprijsOptie, decimal biedenVanafBedrag, decimal prijsBedrag,
                       bool payPal, string rubriekNummer, string advertentieTekst, string websiteUrl, string naamBijAdvertentie )
                       : base(rubriekNummer, advertentieTekst, websiteUrl, naamBijAdvertentie)
        {
            this.Titel = titel;
            this.PrijsType = prijsType;
            this.VraagprijsOptie = vraagprijsOptie;
            this.BiedenVanafBedrag = biedenVanafBedrag;
            this.PrijsBedrag = prijsBedrag;
            this.PayPal = payPal;
        }
    }
}