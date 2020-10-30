using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tabby_Docker.Models;

namespace Tabby_Docker.Data
{
    public class Tabby_DockerContext : DbContext
    {
        public Tabby_DockerContext (DbContextOptions<Tabby_DockerContext> options)
            : base(options)
        {
        }

        public DbSet<Tabby_Docker.Models.Bookmark> Bookmark { get; set; }
    }
}
