using Microsoft.EntityFrameworkCore;


namespace EMP.Data
{
    public partial class EmpContext : DbContext
    {       

        public virtual DbSet<EmpGroupList> EmpGroupList { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeGroup> EmployeeGroup { get; set; }
        public virtual DbSet<EmployeeTechnology> EmployeeTechnology { get; set; }
        public virtual DbSet<Shipment> Shipment { get; set; }
        public virtual DbSet<SchemeProfitLoss> SchemeProfitLoss { get; set; }

        public EmpContext(DbContextOptions<EmpContext> options)
            : base(options)
        {

        }
        public EmpContext()
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

            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.ToTable("Shipment");
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.EmpId).IsRequired();
                entity.Property(e => e.Expiry).HasColumnType("datetime");
                entity.Property(e => e.Password).IsRequired().HasMaxLength(256);
                entity.Property(e => e.Password2).IsRequired().HasMaxLength(256);
                entity.Property(e => e.Broker).IsRequired();
                entity.Property(e => e.LoginId).IsRequired().HasMaxLength(256);
                entity.Property(e => e.Modified).HasColumnType("datetime");
                entity.Property(e => e.Created).HasColumnType("datetime");
            });

            modelBuilder.Entity<SchemeProfitLoss>(entity =>
            {
                entity.ToTable("SchemeProfitLoss");
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.GroupId).IsRequired();
                entity.Property(e => e.Date).HasColumnType("datetime");
                entity.Property(e => e.ProfitLoss).IsRequired();
                entity.Property(e => e.Expense).IsRequired();
                entity.Property(e => e.IsNoTradeDay).IsRequired();
                entity.Property(e => e.IsHoliday).IsRequired();
                entity.Property(e => e.Modified).HasColumnType("datetime");
                entity.Property(e => e.Created).HasColumnType("datetime");
            });

            modelBuilder.Entity<EmpGroupList>(entity =>
            {
                entity.ToTable("EmpGroupList");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Age).HasMaxLength(256);

                entity.Property(e => e.City).HasMaxLength(256);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.DateOfBrith).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Gender).HasMaxLength(256);

                entity.Property(e => e.ImageURL)
                    .HasMaxLength(256)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.LinkedinURL)
                    .HasMaxLength(256)
                    .HasColumnName("LinkedinURL");

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<EmployeeGroup>(entity =>
            {
                entity.ToTable("EmployeeGroup");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(550);

                entity.Property(e => e.IconImg)  
                    .HasMaxLength(256);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<EmployeeTechnology>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.EmployeeTechnologies)
                    .HasForeignKey(d => d.EmpId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
