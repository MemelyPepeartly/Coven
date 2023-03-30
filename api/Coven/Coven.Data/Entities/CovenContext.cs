using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Coven.Data.Entities;

public partial class CovenContext : DbContext
{
    public CovenContext()
    {
    }

    public CovenContext(DbContextOptions<CovenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attribute> Attributes { get; set; }

    public virtual DbSet<AttributeCategory> AttributeCategories { get; set; }

    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<CustomFeature> CustomFeatures { get; set; }

    public virtual DbSet<Feature> Features { get; set; }

    public virtual DbSet<FeatureCategory> FeatureCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attribute>(entity =>
        {
            entity.HasKey(e => e.AttributeId).HasName("PK__Attribut__03B803F0C2BD637F");

            entity.ToTable("Attribute", "app");

            entity.Property(e => e.AttributeId)
                .ValueGeneratedNever()
                .HasColumnName("attributeId");
            entity.Property(e => e.AttributeCategoryId).HasColumnName("attributeCategoryId");
            entity.Property(e => e.AttributeName)
                .HasMaxLength(25)
                .HasColumnName("attributeName");
            entity.Property(e => e.AttributeValue)
                .HasMaxLength(35)
                .HasColumnName("attributeValue");

            entity.HasOne(d => d.AttributeCategory).WithMany(p => p.Attributes)
                .HasForeignKey(d => d.AttributeCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Attribute__attri__6C190EBB");
        });

        modelBuilder.Entity<AttributeCategory>(entity =>
        {
            entity.HasKey(e => e.AttributeCategoryId).HasName("PK__Attribut__7CEBDAB96B63EABE");

            entity.ToTable("AttributeCategory", "app");

            entity.Property(e => e.AttributeCategoryId)
                .ValueGeneratedNever()
                .HasColumnName("attributeCategoryId");
            entity.Property(e => e.AttributeCategoryDescription)
                .HasMaxLength(150)
                .HasColumnName("attributeCategoryDescription");
            entity.Property(e => e.AttributeCategoryName)
                .HasMaxLength(25)
                .HasColumnName("attributeCategoryName");
        });

        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.CharacterId).HasName("PK__Characte__ADF919BF69C0AE76");

            entity.ToTable("Character", "app");

            entity.Property(e => e.CharacterId)
                .ValueGeneratedNever()
                .HasColumnName("characterId");
            entity.Property(e => e.CharacterName)
                .HasMaxLength(50)
                .HasColumnName("characterName");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Characters)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Character__userI__5FB337D6");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Client__CB9A1CFFB1E481F1");

            entity.ToTable("Client", "app");

            entity.HasIndex(e => e.Email, "UQ__Client__AB6E6164A6203F85").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("userId");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<CustomFeature>(entity =>
        {
            entity.HasKey(e => e.CustomFeatureId).HasName("PK__CustomFe__FE3D3D28F6112E01");

            entity.ToTable("CustomFeature", "app");

            entity.Property(e => e.CustomFeatureId)
                .ValueGeneratedNever()
                .HasColumnName("customFeatureId");
            entity.Property(e => e.CustomFeatureName)
                .HasMaxLength(25)
                .HasColumnName("customFeatureName");
            entity.Property(e => e.FeatureCategoryId).HasColumnName("featureCategoryId");

            entity.HasOne(d => d.FeatureCategory).WithMany(p => p.CustomFeatures)
                .HasForeignKey(d => d.FeatureCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CustomFea__featu__6754599E");
        });

        modelBuilder.Entity<Feature>(entity =>
        {
            entity.HasKey(e => e.FeatureId).HasName("PK__Feature__F4F118B3E6A3718B");

            entity.ToTable("Feature", "app");

            entity.Property(e => e.FeatureId)
                .ValueGeneratedNever()
                .HasColumnName("featureId");
            entity.Property(e => e.FeatureCategoryId).HasColumnName("featureCategoryId");
            entity.Property(e => e.FeatureName)
                .HasMaxLength(25)
                .HasColumnName("featureName");

            entity.HasOne(d => d.FeatureCategory).WithMany(p => p.Features)
                .HasForeignKey(d => d.FeatureCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feature__feature__6477ECF3");
        });

        modelBuilder.Entity<FeatureCategory>(entity =>
        {
            entity.HasKey(e => e.FeatureCategoryId).HasName("PK__FeatureC__B90A9A93AC60A894");

            entity.ToTable("FeatureCategory", "app");

            entity.Property(e => e.FeatureCategoryId)
                .ValueGeneratedNever()
                .HasColumnName("featureCategoryId");
            entity.Property(e => e.FeatureCategoryDescription)
                .HasMaxLength(150)
                .HasColumnName("featureCategoryDescription");
            entity.Property(e => e.FeatureCategoryName)
                .HasMaxLength(25)
                .HasColumnName("featureCategoryName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
