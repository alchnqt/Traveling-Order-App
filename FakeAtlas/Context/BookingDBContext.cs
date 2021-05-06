using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FakeAtlas
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

        public virtual DbSet<AvailableOrder> AvailableOrders { get; set; }
        public virtual DbSet<BookingUser> BookingUsers { get; set; }
        public virtual DbSet<OrderUser> OrderUsers { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<UniqueAddress> UniqueAddresses { get; set; }
        public virtual DbSet<UserOrder> UserOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-477QQK7;Database=BookingDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<AvailableOrder>(entity =>
            {
                entity.ToTable("available_orders");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.DepartureTime)
                    .HasColumnType("datetime")
                    .HasColumnName("departure_time");

                entity.Property(e => e.PathFrom)
                    .HasMaxLength(100)
                    .HasColumnName("path_from");

                entity.Property(e => e.PathTo)
                    .HasMaxLength(100)
                    .HasColumnName("path_to");
            });

            modelBuilder.Entity<BookingUser>(entity =>
            {
                entity.ToTable("booking_user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .HasColumnName("full_name");

                entity.Property(e => e.IdAddress).HasColumnName("id_address");

                entity.Property(e => e.IsAdmin).HasColumnName("is_admin");

                entity.Property(e => e.Salt)
                    .HasMaxLength(256)
                    .HasColumnName("salt");

                entity.Property(e => e.UserLogin)
                    .HasMaxLength(256)
                    .HasColumnName("user_login");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(256)
                    .HasColumnName("user_password");

                entity.HasOne(d => d.IdAddressNavigation)
                    .WithMany(p => p.BookingUsers)
                    .HasForeignKey(d => d.IdAddress)
                    .HasConstraintName("fk_idx_address");
            });

            modelBuilder.Entity<OrderUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("order_user");

                entity.Property(e => e.IdOrder).HasColumnName("id_order");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdOrder)
                    .HasConstraintName("pk_id_order");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("pk_id_user");
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.ToTable("shippers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .HasColumnName("full_name");

                entity.Property(e => e.VehicleNum).HasColumnName("vehicle_num");

                entity.Property(e => e.VehicleType)
                    .HasMaxLength(100)
                    .HasColumnName("vehicle_type");
            });

            modelBuilder.Entity<UniqueAddress>(entity =>
            {
                entity.ToTable("unique_address");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BuildingNum).HasColumnName("building_num");

                entity.Property(e => e.City)
                    .HasMaxLength(20)
                    .HasColumnName("city");

                entity.Property(e => e.Oblast)
                    .HasMaxLength(20)
                    .HasColumnName("oblast");

                entity.Property(e => e.StreetName)
                    .HasMaxLength(20)
                    .HasColumnName("street_name");
            });

            modelBuilder.Entity<UserOrder>(entity =>
            {
                entity.ToTable("user_orders");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AvailableOrdersId).HasColumnName("available_orders_id");

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.OrderTime)
                    .HasColumnType("datetime")
                    .HasColumnName("order_time");

                entity.Property(e => e.ShipperId).HasColumnName("shipper_id");

                entity.HasOne(d => d.AvailableOrders)
                    .WithMany(p => p.UserOrders)
                    .HasForeignKey(d => d.AvailableOrdersId)
                    .HasConstraintName("fk_available_orders_id");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.UserOrders)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("fk_client_id");

                entity.HasOne(d => d.Shipper)
                    .WithMany(p => p.UserOrders)
                    .HasForeignKey(d => d.ShipperId)
                    .HasConstraintName("fk_shipper_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
