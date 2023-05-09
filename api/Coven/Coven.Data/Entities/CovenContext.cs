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

    public virtual DbSet<PineconeVectorMetadatum> PineconeVectorMetadata { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<World> Worlds { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PineconeVectorMetadatum>(entity =>
        {
            entity.HasKey(e => e.EntryId).HasName("PK__Pinecone__D124D3D5912348B8");

            entity.ToTable("PineconeVectorMetadata", "app");

            entity.Property(e => e.EntryId)
                .ValueGeneratedNever()
                .HasColumnName("entryId");
            entity.Property(e => e.ArticleId).HasColumnName("articleId");
            entity.Property(e => e.CharacterString)
                .HasMaxLength(1000)
                .HasColumnName("characterString");
            entity.Property(e => e.WorldId).HasColumnName("worldId");

            entity.HasOne(d => d.World).WithMany(p => p.PineconeVectorMetadata)
                .HasForeignKey(d => d.WorldId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PineconeV__world__1CBC4616");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__CB9A1CFFE264D481");

            entity.ToTable("User", "app");

            entity.HasIndex(e => e.Email, "UQ__User__AB6E616402327E44").IsUnique();

            entity.HasIndex(e => e.WorldAnvilUsername, "UQ__User__D93C724BFD54A29B").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("userId");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.WorldAnvilUsername)
                .HasMaxLength(50)
                .HasColumnName("worldAnvilUsername");
        });

        modelBuilder.Entity<World>(entity =>
        {
            entity.HasKey(e => e.WorldId).HasName("PK__World__3E5BA34512714AE2");

            entity.ToTable("World", "app");

            entity.Property(e => e.WorldId)
                .ValueGeneratedNever()
                .HasColumnName("worldId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.WorldName)
                .HasMaxLength(100)
                .HasColumnName("worldName");

            entity.HasOne(d => d.User).WithMany(p => p.Worlds)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__World__userId__19DFD96B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
