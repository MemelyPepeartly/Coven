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

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WacharacterSet> WacharacterSets { get; set; }

    public virtual DbSet<Waembedding> Waembeddings { get; set; }

    public virtual DbSet<Wasnippet> Wasnippets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__CB9A1CFFA45B86B5");

            entity.ToTable("User", "app");

            entity.HasIndex(e => e.Email, "UQ__User__AB6E616410269FDD").IsUnique();

            entity.HasIndex(e => e.WorldAnvilUsername, "UQ__User__D93C724B0D0C2529").IsUnique();

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

        modelBuilder.Entity<WacharacterSet>(entity =>
        {
            entity.HasKey(e => e.CharacterSetId).HasName("PK__WACharac__73ED7C93F85ABE3F");

            entity.ToTable("WACharacterSet", "app");

            entity.Property(e => e.CharacterSetId)
                .ValueGeneratedNever()
                .HasColumnName("characterSetId");
            entity.Property(e => e.CharacterSet).HasColumnName("characterSet");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.WacharacterSets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WACharact__userI__76969D2E");
        });

        modelBuilder.Entity<Waembedding>(entity =>
        {
            entity.HasKey(e => e.EmbeddingId).HasName("PK__WAEmbedd__92ED47F4497F0E0C");

            entity.ToTable("WAEmbedding", "app");

            entity.Property(e => e.EmbeddingId)
                .ValueGeneratedNever()
                .HasColumnName("embeddingId");
            entity.Property(e => e.CharacterSetId).HasColumnName("characterSetId");
            entity.Property(e => e.Vector).HasColumnName("vector");

            entity.HasOne(d => d.CharacterSet).WithMany(p => p.Waembeddings)
                .HasForeignKey(d => d.CharacterSetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WAEmbeddi__chara__797309D9");
        });

        modelBuilder.Entity<Wasnippet>(entity =>
        {
            entity.HasKey(e => e.SnippetId).HasName("PK__WASnippe__C4FEC9170E87021C");

            entity.ToTable("WASnippet", "app");

            entity.Property(e => e.SnippetId)
                .ValueGeneratedNever()
                .HasColumnName("snippetId");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.WorldAnvilArticleId).HasColumnName("worldAnvilArticleId");

            entity.HasOne(d => d.User).WithMany(p => p.Wasnippets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WASnippet__userI__73BA3083");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
