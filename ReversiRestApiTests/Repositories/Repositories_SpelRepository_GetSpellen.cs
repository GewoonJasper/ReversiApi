//using NUnit.Framework;
//using ReversieISpelImplementatie.Model;
//using ReversiRestApi.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ReversiRestApiTests.Repositories
//{
//    [TestFixture]
//    internal class Repositories_SpelRepository_GetSpellen
//    {
//        private Spel2Repository _spelRepository;
//        private List<Spel> Spellen { get; set; }

//        [SetUp]
//        public void SetUp()
//        {
//            _spelRepository = new Spel2Repository();

//            Spel spel1 = new Spel();
//            Spel spel2 = new Spel();
//            Spel spel3 = new Spel();

//            spel1.Speler1Token = "abcdef";
//            spel1.Omschrijving = "Potje snel reveri, dus niet lang nadenken";
//            spel2.Speler1Token = "ghijkl";
//            spel2.Speler2Token = "mnopqr";
//            spel2.Omschrijving = "Ik zoek een gevorderde tegenspeler!";
//            spel3.Speler1Token = "stuvwx";
//            spel3.Omschrijving = "Na dit spel wil ik er nog een paar spelen tegen zelfde tegenstander";


//            Spellen = new List<Spel> { spel1, spel2, spel3 };
//        }

//        [Test]
//        public void GetSpellen_ReturnListMetSpellen()
//        {
//            List<Spel> spellenFromRepo = _spelRepository.GetSpellen();
//            Assert.IsNotNull(spellenFromRepo);

//            int x = 0;
//            foreach (Spel spel in Spellen)
//            {
//                Assert.AreEqual(spel.Speler1Token, spellenFromRepo[x].Speler1Token);
//                Assert.AreEqual(spel.Omschrijving, spellenFromRepo[x].Omschrijving);
//                Assert.AreEqual(spel.Speler2Token, spellenFromRepo[x].Speler2Token);

//                x++;
//            }
//        }
//    }
//}
