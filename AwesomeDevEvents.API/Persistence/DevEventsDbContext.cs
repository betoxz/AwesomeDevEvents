using AwesomeDevEvents.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AwesomeDevEvents.API.Persistence
{
    public class DevEventsDbContext : DbContext
    {
        public DevEventsDbContext(DbContextOptions<DevEventsDbContext> options) : base(options)
        {


        }
        public DbSet<DevEvent> DevEvents { get; set; }
        public DbSet<DevEventSpeaker> DevEventSpeakers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DevEvent>(e =>
            {
                //definição de campos
                e.HasKey(de => de.Id);

                e.Property(de => de.Title).IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

                e.Property(de => de.Description)
                .HasMaxLength(200)
                .HasColumnType("varchar(200)");

                e.Property(de => de.StartDate)
                .HasColumnName("Start_Date");

                e.Property(de => de.EndDate)
                .HasColumnName("End_Date");

                //definição de relacionamento com chave estrangeira
                e.HasMany(de => de.Speakers)
                .WithOne()
                .HasForeignKey(de => de.DevEventId);


            });

            modelBuilder.Entity<DevEventSpeaker>(e =>
            {
                e.HasKey(des => des.Id);

                e.Property(de => de.Name).IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");


                e.Property(de => de.TalkTitle).IsRequired(false)
                .HasMaxLength(200)
                .HasColumnType("varchar(200)");

                e.Property(de => de.TalkDescription).IsRequired(false)
                .HasMaxLength(500)
                .HasColumnType("varchar(500)");

                e.Property(de => de.LinkedInProfile).IsRequired(false)
                .HasMaxLength(200)
                .HasColumnType("varchar(200)");

            });
            base.OnModelCreating(modelBuilder);
        }

    }
}
