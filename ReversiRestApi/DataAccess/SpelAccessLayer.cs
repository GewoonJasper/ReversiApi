using Microsoft.EntityFrameworkCore;
using ReversieISpelImplementatie.Model;
using ReversiRestApi.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReversiRestApi.DataAccess
{
    public class SpelAccessLayer : ISpelRepository
    {
        private readonly SpelDbContext _context;

        public SpelAccessLayer(SpelDbContext context) => _context = context;

        public void AddSpel(Spel spel)
        {
            _context.Add(spel);
        }

        public void DeleteSpel(Spel spel)
        {
            _context.Remove(spel);
        }

        public async Task<Spel> GetSpel(string spelToken)
        {
            List<Spel> spellen = await _context.Spellen.ToListAsync();
            Spel spel = spellen.Where(s => s.Token == spelToken).FirstOrDefault();
            if (spel == null) return null;
            spel.ConvertStringToBord();

            return spel;
        }

        public async Task<List<Spel>> GetSpellen()
        {
            return await _context.Spellen.ToListAsync();
        }

        public void SaveSpellen()
        {
            _context.SaveChanges();
        }
    }
}
