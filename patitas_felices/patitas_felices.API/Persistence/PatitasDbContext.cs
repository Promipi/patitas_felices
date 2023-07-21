
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using patitas_felices.Common.Models.Feeder;
using patitas_felices.Common.Models.Photo;
using patitas_felices.Common.Models.User;

namespace patitas_felices.API.Persistence
{
    public class PatitasDbContext : IdentityDbContext<User>
    {
        public PatitasDbContext(DbContextOptions<PatitasDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Feeder> Feeders {get;set;}

        public DbSet<Photo> Photos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
