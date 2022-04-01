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
//    internal class Repositories_SpelRepository_GetSpel
//    {
//        private Spel2Repository _spelRepository;

//        [SetUp]
//        public void SetUp()
//        {
//            _spelRepository = new Spel2Repository();
//        }

//        [Test]
//        public void GetSpel_TokenGiven_ReturnSpel()
//        {
//            List<Spel> spellen = _spelRepository.GetSpellen();

//            var spel1Token = spellen[0].Token;
//            var spel2Token = spellen[1].Token;
//            var spel3Token = spellen[2].Token;

//            Assert.AreEqual(spellen[0], _spelRepository.GetSpelFromToken(spel1Token));
//            Assert.AreEqual(spellen[1], _spelRepository.GetSpelFromToken(spel2Token));
//            Assert.AreEqual(spellen[2], _spelRepository.GetSpelFromToken(spel3Token));
//        }
//    }
//}
