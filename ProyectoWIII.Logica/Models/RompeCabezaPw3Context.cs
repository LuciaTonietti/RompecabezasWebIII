using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Rompecabezas.Logica.Models;

public partial class RompeCabezaPw3Context : DbContext
{
    public RompeCabezaPw3Context()
    {
    }

    public RompeCabezaPw3Context(DbContextOptions<RompeCabezaPw3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Sala> Salas { get; set; }

    public virtual DbSet<ScoreMap> ScoreMaps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-2VFKTQS\\SQLEXPRESS02; Database=RompeCabezaPW3; TrustServerCertificate=True; User Id=sa; Password=password;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sala>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sala__3213E83F3FEE6F19");

            entity.ToTable("Sala");

            entity.HasIndex(e => e.NickName, "UQ__Sala__48F06EC1D4A1B931").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CantPieces).HasColumnName("cant_pieces");
            entity.Property(e => e.NickName)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("nickName");
            entity.Property(e => e.Pin)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("pin");
            entity.Property(e => e.ScoreMap).HasColumnName("score_map");

            entity.HasOne(d => d.ScoreMapNavigation).WithMany(p => p.Salas)
                .HasForeignKey(d => d.ScoreMap)
                .HasConstraintName("FK__Sala__score_map__3B75D760");
        });

        modelBuilder.Entity<ScoreMap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ScoreMap__3213E83FCE42F81D");

            entity.ToTable("ScoreMap");

            entity.HasIndex(e => e.NickName, "UQ__ScoreMap__48F06EC1D8A6FC91").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NickName)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("nickName");
            entity.Property(e => e.Score).HasColumnName("score");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
