using System;
using System.Collections.Generic;
using CPBLDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace CPBLDemo.DataAccess.Data;

public partial class dbCpblBaseBallContext : DbContext
{
    public dbCpblBaseBallContext(DbContextOptions<dbCpblBaseBallContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PlayerList> PlayerList { get; set; }

    public virtual DbSet<Team> Team { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlayerList>(entity =>
        {
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.FirstDate).HasColumnType("datetime");
            entity.Property(e => e.Height).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Position)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.TeamId).HasColumnName("TeamID");
            entity.Property(e => e.Weight).HasColumnType("decimal(18, 3)");

            entity.HasOne(d => d.Team).WithMany(p => p.PlayerList)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlayerList_Team");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.Property(e => e.TeamId)
                .ValueGeneratedNever()
                .HasColumnName("TeamID");
            entity.Property(e => e.TeamName)
                .IsRequired()
                .HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}