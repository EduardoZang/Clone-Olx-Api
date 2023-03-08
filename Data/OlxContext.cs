using OlxApi.Models;
using Microsoft.EntityFrameworkCore;

namespace OlxApi.Data{

    public class OlxContext : DbContext {
        public OlxContext(DbContextOptions<OlxContext>opts): base(opts) {
                
        }  
        public DbSet<Ad> Ads { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Image> Images { get; set; }

    }
}