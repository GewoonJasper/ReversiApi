using Microsoft.EntityFrameworkCore;
using ReversieISpelImplementatie.Model;
using ReversiRestApi.Models;

namespace ReversiRestApi.DataAccess
{
    public class SpelDbContext : DbContext
    {
        public SpelDbContext(DbContextOptions<SpelDbContext> options) : base(options)
        {
               
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Spel>()
                .HasKey(e => e.ID);

            modelBuilder.Entity<Spel>()
                .Property(s => s.BordInString)
                .HasColumnName("Bord");
        }

        //Hieronder alle Models plaatsen die EntityFramework moet implementeren
        public DbSet<Spel> Spellen { get; set; }
    }
}
