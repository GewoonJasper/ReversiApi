using ReversieISpelImplementatie.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReversiRestApi.Models
{
    public class SpelTbvJson
    {
        public int ID { get; set; }
        public string Omschrijving { get; set; }
        public string Token { get; set; }
        public string Speler1Token { get; set; }
        public string Speler2Token { get; set; }

        public string Bord { get; set; }

        public Kleur AandeBeurt { get; set; }
        public int Beurt { get; set; }

        public string AantalFichesWitPerBeurt { get; set; }

        public string AantalFichesZwartPerBeurt { get; set; }

        public Status Status { get; set; }

        public Kleur Winnaar { get; set; }
        
        public SpelTbvJson(Spel spel)
        {
            ID = spel.ID;
            Omschrijving = spel.Omschrijving;
            Token = spel.Token;
            Speler1Token = spel.Speler1Token;
            Speler2Token = spel.Speler2Token;
            AandeBeurt = spel.AandeBeurt;
            Status = spel.Status;
            Winnaar = spel.Winnaar;
            Bord = spel.BordInString;
            Beurt = spel.Beurt;
            AantalFichesWitPerBeurt = spel.AantalFichesWitPerBeurt;
            AantalFichesZwartPerBeurt = spel.AantalFichesZwartPerBeurt;
        }
    }
}
