using Microsoft.EntityFrameworkCore;
using SBMS.src.Entitiies;

namespace SBMS.src.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Service> Services { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<ServiceToken> ServiceTokens { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship between Service and Subscriber
            modelBuilder.Entity<Service>()
                .HasMany(s => s.Subscribers)
                .WithOne(sub => sub.Service)
                .HasForeignKey(sub => sub.ServiceId)
                .HasPrincipalKey(s => s.Id); // Use the Id from BaseEntity as the principal key

            // Configure ServiceToken relationship
            modelBuilder.Entity<ServiceToken>()
                .HasOne(st => st.Service)
                .WithMany() // No navigation property back from Service
                .HasForeignKey(st => st.ServiceId)
                .HasPrincipalKey(s => s.Id);

            // Set table names to match the class names by convention
            // EF Core does this by default, but you can be explicit if needed
            modelBuilder.Entity<Service>().ToTable("Services");
            modelBuilder.Entity<Subscriber>().ToTable("Subscribers");
            modelBuilder.Entity<ServiceToken>().ToTable("ServiceTokens");
            modelBuilder.Entity<Subscription>().ToTable("Subscriptions");
        }
    }
}