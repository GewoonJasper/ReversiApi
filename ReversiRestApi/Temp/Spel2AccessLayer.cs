//using Microsoft.AspNetCore.Mvc;
//using ReversieISpelImplementatie.Model;
//using ReversiRestApi.Models;
//using ReversiRestApi.Repositories;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ReversiRestApi.DataAccess
//{
//    public class Spel2AccessLayer : ISpel2Repository
//    {
//        public Spel2AccessLayer(SpelDbContext context) => _context = context;

//        private readonly SpelDbContext _context;

//        public async void AddSpel(Spel spel)
//        {
//            //_context.Add(new SpelTbvJson(spel, true));
//            _context.Add(spel);
//            _context.SaveChanges();
//        }

//        public Spel GetSpelFromToken(string spelToken)
//        {
//            //return SpelTbvJson.toSpel(_context.Spellen.Find(spelToken));

//            //Spel spel = _context.Spellen.Where(s => s.Token == spelToken).FirstOrDefault();
//            Spel spel = _context.Spellen.First(s => s.Token == spelToken);
//            spel.ConvertStringToBord();


//            return spel;
//        }

//        public Spel GetSpelFromSpelerToken(string spelerToken)
//        {
//            return _context.Spellen.Where(s => s.Speler1Token == spelerToken || s.Speler2Token == spelerToken).FirstOrDefault();
//        }

//        public List<Spel> GetSpellen()
//        {
//            //return SpelTbvJson.toSpellen(_context.Spellen.ToList());

//            return _context.Spellen.ToList();
//        }

//        public Spel Join(string spelToken, string spelerToken)
//        {
//            Spel spel = _context.Spellen.Where(s => s.Token == spelToken).FirstOrDefault();
//            spel.Speler2Token = spelerToken;
//            spel.AandeBeurt = Kleur.Wit;
//            _context.SaveChanges();

//            return spel;
//        }

//        public void UpdateSpel(Spel spel)
//        {
//            _context.Update(spel);
//        }

//        public void SaveChanges()
//        {
//            _context.SaveChanges();
//        }

//        public void DeleteSpel(Spel spel)
//        {
//            _context.Spellen.Remove(spel);
//        }
//    }
//}
