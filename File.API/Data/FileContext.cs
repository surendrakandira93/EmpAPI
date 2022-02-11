using File.API.Dto;
using Microsoft.EntityFrameworkCore;

namespace File.API.Data
{
    public partial class FileContext : DbContext
    {
        public virtual DbSet<FileTable> FileTable { get; set; }

        public FileContext(DbContextOptions<FileContext> options)
           : base(options)
        {

        }
        public FileContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source={DBKeys.BaseDatabasePath}");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<FileTable>(entity =>
            {
                entity.ToTable("FileTable");
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Key).IsRequired();
                entity.Property(e => e.Value).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
