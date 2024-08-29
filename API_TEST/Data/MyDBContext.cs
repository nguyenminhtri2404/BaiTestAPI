using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API_TEST.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Recording> Recordings { get; set; }
        public DbSet<PlaybackSchedule> PlaybackSchedules { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Format> Formats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recording>()
                .HasOne(r => r.Genre)
                .WithMany(g => g.Recordings)
                .HasForeignKey(r => r.GenreID);

            modelBuilder.Entity<Recording>()
                .HasOne(r => r.Format)
                .WithMany(f => f.Recordings)
                .HasForeignKey(r => r.FormatID);

            modelBuilder.Entity<PlaybackSchedule>()
                .HasOne(ps => ps.Recording)
                .WithMany(r => r.PlaybackSchedules)
                .HasForeignKey(ps => ps.RecordingID);

            ValueConverter<List<DayOfWeek>, string> daysOfWeekConverter = new(
                  v => string.Join(',', v.Select(d => d.ToString())),
                  v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(d => Enum.Parse<DayOfWeek>(d)).ToList());

            modelBuilder.Entity<PlaybackSchedule>()
                .Property(e => e.RepeatDays)
                .HasConversion(daysOfWeekConverter);
        }

    }
}
