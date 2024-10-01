using System;
using System.Collections.Generic;
using CPBLDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace CPBLDemo.Data;

public partial class dbCpblBaseBallContext : DbContext
{
    public dbCpblBaseBallContext(DbContextOptions<dbCpblBaseBallContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PlayerList> PlayerList { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlayerList>(entity =>
        {
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.FirstDate).HasColumnType("datetime");
            entity.Property(e => e.Height).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Position).HasMaxLength(50);
            entity.Property(e => e.Weight).HasColumnType("decimal(18, 3)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
