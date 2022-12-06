using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sugarbakers.Models;

public partial class SugarbakersDbContext : DbContext
{
    public SugarbakersDbContext()
    {
    }

    public SugarbakersDbContext(DbContextOptions<SugarbakersDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<ItemsonOrder> ItemsonOrders { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Zip> Zips { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectModels;Database=SugarbakersDB;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK_customers");

            entity.Property(e => e.CustomerId)
                .ValueGeneratedNever()
                .HasColumnName("CustomerID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Extension).HasMaxLength(10);
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(30);
            entity.Property(e => e.Zipcode).HasMaxLength(20);

            entity.HasOne(d => d.ZipcodeNavigation).WithMany(p => p.Customers)
                .HasForeignKey(d => d.Zipcode)
                .HasConstraintName("FK_customersZip");
        });

        modelBuilder.Entity<ItemsonOrder>(entity =>
        {
            entity.HasKey(e => new { e.OrdersId, e.ProductsId }).HasName("PK_itemsonorder");

            entity.ToTable("ItemsonOrder");

            entity.Property(e => e.OrdersId).HasColumnName("OrdersID");
            entity.Property(e => e.ProductsId).HasColumnName("ProductsID");
            entity.Property(e => e.LineItemTotal)
                .HasComputedColumnSql("([UnitPrice]*[Quantity])", false)
                .HasColumnType("money");
            entity.Property(e => e.ShipDate).HasColumnType("datetime");
            entity.Property(e => e.UnitPrice).HasColumnType("money");

            entity.HasOne(d => d.Orders).WithMany(p => p.ItemsonOrders)
                .HasForeignKey(d => d.OrdersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdersItems");

            entity.HasOne(d => d.Products).WithMany(p => p.ItemsonOrders)
                .HasForeignKey(d => d.ProductsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductItems");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrdersId).HasName("PK_orders");

            entity.Property(e => e.OrdersId)
                .ValueGeneratedNever()
                .HasColumnName("OrdersID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.FreightCharge).HasColumnType("money");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.TotalDue).HasColumnType("money");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdersCustomer");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => new { e.CustomerId, e.PmtDate }).HasName("PK_payments");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.PmtDate).HasColumnType("datetime");
            entity.Property(e => e.Amt).HasColumnType("money");

            entity.HasOne(d => d.Customer).WithMany(p => p.Payments)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaymentsCustomer");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductsId);

            entity.Property(e => e.ProductsId)
                .ValueGeneratedNever()
                .HasColumnName("ProductsID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Zip>(entity =>
        {
            entity.HasKey(e => e.Zipcode).HasName("PK_zipcode");

            entity.ToTable("Zip");

            entity.Property(e => e.Zipcode).HasMaxLength(20);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.State).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
