using Microsoft.EntityFrameworkCore;

namespace Tabby_Docker.Data
{
    public class Tabby_DockerContext : DbContext
    {
        public Tabby_DockerContext(DbContextOptions<Tabby_DockerContext> options)
            : base(options)
        {
        }

        public DbSet<Tabby_Docker.Models.Bookmark> Bookmark { get; set; }
        
    }
}
