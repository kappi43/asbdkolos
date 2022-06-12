using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions options) : base(options)
        {
        }

        protected MainDbContext()
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<Musician_Track> Musician_Tracks { get; set; }
        public DbSet<MusicLabel> MusicLabels { get; set; }
        public DbSet<Track> Tracks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Album>(p =>
            {
                p.HasKey(e => e.IdAlbum);
                p.Property(e => e.PublishDate).IsRequired();
                p.Property(e => e.AlbumName).IsRequired();
                p.HasOne(e => e.MusicLabel).WithMany(e => e.Album).HasForeignKey(e => e.IdMusicLabel);
                p.HasMany(e => e.Track).WithOne(e => e.Album).HasForeignKey(e => e.IdMusicAlbum);

                p.HasData(
                    new Album { IdAlbum = 1, AlbumName = "test", PublishDate = DateTime.Now, IdMusicLabel = 1 }
                    );
            }
            );

            modelBuilder.Entity<Musician>(p =>
            {
                p.HasKey(e => e.IdMusician);
                p.Property(e => e.FirstName).IsRequired();
                p.Property(e => e.LastName).IsRequired();
                p.Property(e => e.Nickname).IsRequired(false);
                p.HasMany(e => e.Musician_Track).WithOne(e => e.Musician).HasForeignKey(e => e.IdMusician);
                p.HasData(new Musician { IdMusician = 1, FirstName = "Kacper", LastName = "Czajkowski", Nickname = "test" });
            }
            );
            modelBuilder.Entity<Musician_Track>(p =>
            {
                p.HasKey(e => new { e.IdMusician, e.IdTrack });
                p.HasData(new Musician_Track { IdMusician=1,IdTrack=1});

            });
            modelBuilder.Entity<MusicLabel>(p => 
            {
                p.HasKey(e => e.IdMusicLabel);
                p.Property(e => e.Name).IsRequired();
                p.HasData(new MusicLabel { IdMusicLabel = 1, Name = "test" });

            });
            modelBuilder.Entity<Track>(p =>
            {
                p.HasKey(e => e.IdTrack);
                p.Property(e => e.TrackName);
                p.Property(e => e.Duration);
                p.HasMany(e => e.Musician_Track).WithOne(e => e.Track).HasForeignKey(e => e.IdTrack).IsRequired(false);
                p.HasData(new Track { IdTrack=1,TrackName="test", Duration=2.3F, IdMusicAlbum=1});
            });

        }
    }
}
