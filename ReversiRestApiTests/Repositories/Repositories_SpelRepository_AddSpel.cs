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
//    internal class Repositories_SpelRepository_AddSpel
//    {
//        private Spel2Repository _spelRepository;
        
//        [SetUp]
//        public void SetUp()
//        {
//            _spelRepository = new Spel2Repository();
//        }
        
//        // Deze test doe ik niet meer omdat de null check in de controller moet zijn
//        //[Test]
//        //public void SpelRepository_AddSpel_NoSpelGiven()
//        //{
//        //    _spelRepository.AddSpel(null);
//        //    Assert.AreEqual(3, _spelRepository.Spellen.Count);
//        //}

//        [Test]
//        public void SpelRepository_AddSpel_SpelGiven()
//        {
//            Spel spel = new Spel() { Speler1Token = "acd", Omschrijving = "TestToken" };

//            _spelRepository.AddSpel(spel);
//            Assert.AreEqual(spel, _spelRepository.Spellen[_spelRepository.Spellen.Count - 1]);
//            Assert.AreEqual(4, _spelRepository.Spellen.Count);
//        }
//    }
//}
