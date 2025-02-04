using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PartnerInteraction.Models
{
    public partial class DemoContext : DbContext
    {
        public DemoContext()
        {
        }

        public DemoContext(DbContextOptions<DemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MaterialType> MaterialTypes { get; set; } = null!;
        public virtual DbSet<Partner> Partners { get; set; } = null!;
        public virtual DbSet<PartnerType> PartnerTypes { get; set; } = null!;
        public virtual DbSet<PartnersProduct> PartnersProducts { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductType> ProductTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=ngknn.ru;Port=5442;Database=41p_schekaleva_demo;Username=31P;Password=12345");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MaterialType>(entity =>
            {
                entity.ToTable("material_types");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DefectsPercentage).HasColumnName("defects_percentage");

                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Partner>(entity =>
            {
                entity.ToTable("partners");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Director)
                    .HasColumnType("character varying")
                    .HasColumnName("director");

                entity.Property(e => e.Email)
                    .HasColumnType("character varying")
                    .HasColumnName("email");

                entity.Property(e => e.Inn)
                    .HasColumnType("character varying")
                    .HasColumnName("inn");

                entity.Property(e => e.LegalAddress)
                    .HasColumnType("character varying")
                    .HasColumnName("legal_address");

                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");

                entity.Property(e => e.PartnerTypeId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("partner_type_id");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.TelephoneNumber)
                    .HasColumnType("character varying")
                    .HasColumnName("telephone_number");

                entity.HasOne(d => d.PartnerType)
                    .WithMany(p => p.Partners)
                    .HasForeignKey(d => d.PartnerTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("partners_partner_types_fk");
            });

            modelBuilder.Entity<PartnerType>(entity =>
            {
                entity.ToTable("partner_types");

                entity.HasIndex(e => e.Name, "partner_types_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");
            });

            modelBuilder.Entity<PartnersProduct>(entity =>
            {
                entity.ToTable("partners_products");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("amount");

                entity.Property(e => e.DateOfSale)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("date_of_sale");

                entity.Property(e => e.PartnerId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("partner_id");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("product_id");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.PartnersProducts)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("partners_products_partners_fk");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PartnersProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("partners_products_products_fk");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.HasIndex(e => e.Articul, "products_articul_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Articul)
                    .HasColumnType("character varying")
                    .HasColumnName("articul");

                entity.Property(e => e.MinCostForPartner).HasColumnName("min_cost_for_partner");

                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");

                entity.Property(e => e.TypeId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("type_id");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("products_product_types_fk");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.ToTable("product_types");

                entity.HasIndex(e => e.TypeName, "product_types_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TypeName)
                    .HasColumnType("character varying")
                    .HasColumnName("type_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
