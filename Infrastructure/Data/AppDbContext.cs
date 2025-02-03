using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Measures> Measures { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D82AF8512E");

                entity.Property(e => e.Address).HasMaxLength(255);
                entity.Property(e => e.Classification).HasMaxLength(255);
                entity.Property(e => e.Company).HasMaxLength(255);
                entity.Property(e => e.CustomerCode).HasMaxLength(20);
                entity.Property(e => e.CustomerName).HasMaxLength(255);
                entity.Property(e => e.Lat).HasColumnType("numeric(9, 6)");
                entity.Property(e => e.Long).HasColumnType("numeric(9, 6)");
                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.HasOne(d => d.Salesman).WithMany(p => p.Customers)
                    .HasPrincipalKey(p => p.UserId)
                    .HasForeignKey(d => d.SalesmanId)
                    .HasConstraintName("FK__Customers__Sales__12C8C788");
            });

            modelBuilder.Entity<Measures>(entity =>
            {
                entity.HasKey(e => e.MeasureId).HasName("PK__Measures__8C56D080F5207B99");

                entity.Property(e => e.Achieved).HasColumnType("numeric(10, 2)");
                entity.Property(e => e.Company).HasMaxLength(255);
                entity.Property(e => e.Expected).HasColumnType("numeric(10, 2)");
                entity.Property(e => e.Measure)
                    .HasMaxLength(20)
                    .HasColumnName("Measure");
                entity.Property(e => e.Remaining).HasColumnType("numeric(10, 2)");
                entity.Property(e => e.Target).HasColumnType("numeric(10, 2)");

                entity.HasOne(d => d.Customer).WithMany(p => p.Measures)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Measures__Custom__10E07F16");

                entity.HasOne(d => d.Salesman).WithMany(p => p.Measures)
                    .HasPrincipalKey(p => p.UserId)
                    .HasForeignKey(d => d.SalesmanId)
                    .HasConstraintName("FK__Measures__Salesm__0FEC5ADD");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0747A7379D");

                entity.HasIndex(e => e.UserId, "UQ__Users__1788CC4D59FD8AAA").IsUnique();

                entity.Property(e => e.Company).HasMaxLength(255);
                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.SalesmanCode).HasMaxLength(20);
                entity.Property(e => e.Title).HasMaxLength(255);
                entity.Property(e => e.UserName).HasMaxLength(255);

                entity.HasOne(d => d.DirectManager).WithMany(p => p.InverseDirectManager)
                    .HasPrincipalKey(p => p.UserId)
                    .HasForeignKey(d => d.DirectManagerId)
                    .HasConstraintName("FK__Users__DirectMan__0A338187");
            });

        }

    }
}