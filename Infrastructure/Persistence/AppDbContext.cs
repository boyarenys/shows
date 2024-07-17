using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Show>(entity =>
            {
                entity.Property(s => s.Id)
                    .ValueGeneratedNever();

                // Relación con Network
                entity.HasOne(s => s.Network)
                    .WithMany()
                    .HasForeignKey(s => s.NetworkId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Relación con Links 
                entity.HasOne(s => s.Links)
                    .WithOne()
                    .HasForeignKey<Show>(s => s.LinkId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Relación con WebChannel
                entity.HasOne(s => s.WebChannel)
                .WithMany()
                .HasForeignKey(s => s.WebChannelId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de Link
            modelBuilder.Entity<Link>(entity =>
            {
                entity.HasKey(l => l.Id);

                // Relación con Self
                entity.HasOne(l => l.Self)
                    .WithMany(s => s.Links)
                    .HasForeignKey(l => l.SelfId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Relación con Previousepisode
                entity.HasOne(l => l.Previousepisode)
                    .WithMany(p => p.Links)
                    .HasForeignKey(l => l.PreviousepisodeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        
            modelBuilder.Entity<Self>(entity =>
            {
                entity.HasKey(s => s.Id);
            });
                     
            modelBuilder.Entity<Previousepisode>(entity =>
            {
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<Network>(entity =>
            {
                entity.HasKey(n => n.Id);
            });

            modelBuilder.Entity<WebChannel>(entity =>
            {
                entity.HasKey(w => w.Id);
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<Previousepisode> Previousepisodes { get; set; }
        public DbSet<Self> Self { get; set; }
        public DbSet<WebChannel> WebChannels { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Externals> Externals { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Network> Networks { get; set; }

    }
}
