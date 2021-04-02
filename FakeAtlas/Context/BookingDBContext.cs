using System;
using FakeAtlas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FakeAtlas.Context
{
    public partial class BookingDBContext : DbContext
    {
        public BookingDBContext()
        {
        }

        public BookingDBContext(DbContextOptions<BookingDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BookingUser> BookingUsers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<UniqueAddress> UniqueAddresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-477QQK7;Database=BookingDB;Trusted_Connection=True;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<BookingUser>(entity =>
            {
                entity.ToTable("booking_user");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("full_name");

                entity.Property(e => e.IdAddress).HasColumnName("id_address");

                entity.Property(e => e.IsAdmin).HasColumnName("is_admin");

                entity.HasOne(d => d.IdAddressNavigation)
                    .WithMany(p => p.BookingUsers)
                    .HasForeignKey(d => d.IdAddress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_idx_address");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.OrderTime)
                    .HasColumnType("datetime")
                    .HasColumnName("order_time");

                entity.Property(e => e.PathName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("path_name");

                entity.Property(e => e.ShipperId).HasColumnName("shipper_id");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_client_id");

                entity.HasOne(d => d.Shipper)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipperId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_shipper_id");
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.ToTable("shippers");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("full_name");

                entity.Property(e => e.VehicleNum).HasColumnName("vehicle_num");
            });

            modelBuilder.Entity<UniqueAddress>(entity =>
            {
                entity.ToTable("unique_address");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.BuildingNum).HasColumnName("building_num");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("city");

                entity.Property(e => e.Oblast)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("oblast");

                entity.Property(e => e.StreetName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("street_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
