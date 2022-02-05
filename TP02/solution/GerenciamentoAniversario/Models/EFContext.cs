using Microsoft.EntityFrameworkCore;

namespace GerenciamentoAniversario.Models
{
    public class EFContext : DbContext
    {
        private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=infnet;Trusted_Connection=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Pessoa> Pessoas { get; set; }

    }
}
