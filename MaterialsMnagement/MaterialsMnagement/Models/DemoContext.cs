using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MaterialsMnagement.Models;

public partial class DemoContext : DbContext
{
    public DemoContext()
    {
    }

    public DemoContext(DbContextOptions<DemoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<MaterialType> MaterialTypes { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Demo2;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("materials_pk");

            entity.ToTable("materials");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.Image)
                .HasColumnType("character varying")
                .HasColumnName("image");
            entity.Property(e => e.MinimalAmount).HasColumnName("minimal_amount");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.PackQuantity).HasColumnName("pack_quantity");
            entity.Property(e => e.Type)
                .ValueGeneratedOnAdd()
                .HasColumnName("type");
            entity.Property(e => e.Unit)
                .HasColumnType("character varying")
                .HasColumnName("unit");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Materials)
                .HasForeignKey(d => d.Type)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("materials_material_types_fk");
        });

        modelBuilder.Entity<MaterialType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("material_types_pk");

            entity.ToTable("material_types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("suppliers_pk");

            entity.ToTable("suppliers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");

            entity.HasMany(d => d.Materials).WithMany(p => p.Suppliers)
                .UsingEntity<Dictionary<string, object>>(
                    "MaterialsSupplier",
                    r => r.HasOne<Material>().WithMany()
                        .HasForeignKey("Material")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("materials_suppliers_materials_fk"),
                    l => l.HasOne<Supplier>().WithMany()
                        .HasForeignKey("Supplier")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("materials_suppliers_suppliers_fk"),
                    j =>
                    {
                        j.HasKey("Supplier", "Material").HasName("materials_suppliers_pk");
                        j.ToTable("materials_suppliers");
                        j.IndexerProperty<int>("Supplier")
                            .ValueGeneratedOnAdd()
                            .HasColumnName("supplier");
                        j.IndexerProperty<int>("Material")
                            .ValueGeneratedOnAdd()
                            .HasColumnName("material");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
