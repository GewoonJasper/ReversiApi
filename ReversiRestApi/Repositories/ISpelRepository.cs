using ReversieISpelImplementatie.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReversiRestApi.Repositories
{
    public interface ISpelRepository
    {
        public void AddSpel(Spel spel);
        public Task<Spel> GetSpel(string spelToken);
        public Task<List<Spel>> GetSpellen();
        public void SaveSpellen();
        public void DeleteSpel(Spel spel);
    }
}
