using DigitalPetitions.Domain;
using DigitalPetitions.Domain.AdminNS;
using DigitalPetitions.Domain.Petitions;
using DigitalPetitions.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DigitalPetitions.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {

        private readonly IWebHostEnvironment _env;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IWebHostEnvironment env) : base(options)
        {
            _env = env;
        }

        public DbSet<Petition> Petitions => Set<Petition>();

        public DbSet<Answer> Answers => Set<Answer>();

        public DbSet<Creator> Creators => Set<Creator>();

        public DbSet<Sign> Signs => Set<Sign>();

        public DbSet<Signee> Signees => Set<Signee>();

        public DbSet<User> Users => Set<User>();

        public DbSet<Admin> Admins => Set<Admin>();

        public DbSet<ModerationResult> ModerationResults => Set<ModerationResult>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (_env.IsDevelopment())
            {
                modelBuilder.Seed();
            }
        }
    }
}
