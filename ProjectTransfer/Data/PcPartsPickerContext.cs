using System;
using System.Collections.Generic;
using Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Data;

public partial class PcPartsPickerContext : DbContext
{
    public PcPartsPickerContext()
    {
    }

    public PcPartsPickerContext(DbContextOptions<PcPartsPickerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Case> Cases { get; set; }

    public virtual DbSet<Cooler> Coolers { get; set; }

    public virtual DbSet<Cpu> Cpus { get; set; }

    public virtual DbSet<Gpu> Gpus { get; set; }

    public virtual DbSet<Motherboard> Motherboards { get; set; }

    public virtual DbSet<Psu> Psus { get; set; }

    public virtual DbSet<Ram> Rams { get; set; }

    public virtual DbSet<Ssd> Ssds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=./;Database=PC_parts_picker;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Case>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cases__3214EC078F9607C4");
        });

        modelBuilder.Entity<Cooler>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coolers__3214EC07BB56AFA4");
        });

        modelBuilder.Entity<Cpu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CPUs__3214EC07D60BC74E");
        });

        modelBuilder.Entity<Gpu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GPUs__3214EC072F547A97");
        });

        modelBuilder.Entity<Motherboard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Motherbo__3214EC07B5A8C486");
        });

        modelBuilder.Entity<Psu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PSUs__3214EC0732E061E0");
        });

        modelBuilder.Entity<Ram>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RAM__3214EC0734981678");
        });

        modelBuilder.Entity<Ssd>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SSDs__3214EC0732E48147");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
