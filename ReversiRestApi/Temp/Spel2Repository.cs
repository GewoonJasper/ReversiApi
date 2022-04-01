//using ReversieISpelImplementatie.Model;
//using System.Collections.Generic;

//namespace ReversiRestApi.Repositories
//{
//    public class Spel2Repository : ISpel2Repository
//    {
//        // Lijst met tijdelijke spellen
//        public List<Spel> Spellen { get; set; }

//        public Spel2Repository()
//        {
//            Spel spel1 = new Spel();
//            Spel spel2 = new Spel();
//            Spel spel3 = new Spel();

//            spel1.Speler1Token = "abcdef";
//            spel1.Omschrijving = "Potje snel reversi, dus niet lang nadenken";
//            spel2.Speler1Token = "ghijkl";
//            spel2.Speler2Token = "mnopqr";
//            spel2.Omschrijving = "Ik zoek een gevorderde tegenspeler!";
//            spel3.Speler1Token = "stuvwx";
//            spel3.Omschrijving = "Na dit spel wil ik er nog een paar spelen tegen zelfde tegenstander";


//            Spellen = new List<Spel> { spel1, spel2, spel3 };
//        }

//        public void AddSpel(Spel spel)
//        {
//            Spellen.Add(spel);
//        }

//        public List<Spel> GetSpellen()
//        {
//            return Spellen;
//        }

//        public Spel GetSpelFromToken(string spelToken)
//        {
//            return Spellen.Find(s => s.Token == spelToken);
//        }

//        public Spel Join(string spelToken, string spelerToken)
//        {
//            throw new System.NotImplementedException();
//        }

//        public Spel GetSpelFromSpelerToken(string spelerToken)
//        {
//            throw new System.NotImplementedException();
//        }

//        public void DeleteSpel(Spel spel)
//        {
//            throw new System.NotImplementedException();
//        }

//        public void UpdateSpel(Spel spel)
//        {
//            throw new System.NotImplementedException();
//        }

//        public void SaveChanges()
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}
