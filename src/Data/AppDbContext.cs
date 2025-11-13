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
            
            modelBuilder.Entity<Service>()
                .HasMany(s => s.Subscribers)
                .WithOne(sub => sub.Service)
                .HasForeignKey(sub => sub.ServiceId)
                .HasPrincipalKey(s => s.Id); 

            modelBuilder.Entity<ServiceToken>()
                .HasOne(st => st.Service)
                .WithMany() 
                .HasForeignKey(st => st.ServiceId)
                .HasPrincipalKey(s => s.Id);
        }
    }
}