using Microsoft.EntityFrameworkCore;

namespace WeChipApi.Model {
    public class AppContext : DbContext {
        public AppContext(DbContextOptions<AppContext> options) : base(options) {
            Database.EnsureCreated();
        }

        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Enderecos> Enderecos { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<Vendas> Vendas { get; set; }
    }
}