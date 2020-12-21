using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Infraestructura.Entidades
{
    public partial class ClientesDBContext : DbContext
    {
        public ClientesDBContext()
        {
        }

        public ClientesDBContext(DbContextOptions<ClientesDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblArticulo> TblArticulos { get; set; }
        public virtual DbSet<TblCliente> TblClientes { get; set; }
        public virtual DbSet<TblClienteArticulo> TblClienteArticulos { get; set; }
        public virtual DbSet<TblGrupo> TblGrupos { get; set; }
        public virtual DbSet<TblPai> TblPais { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=Connection1");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblArticulo>(entity =>
            {
                entity.ToTable("tblArticulo");

                entity.HasIndex(e => e.Codigo, "UQ__tblArtic__06370DACAEFE354B")
                    .IsUnique();

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Precio).HasColumnType("decimal(9, 2)");
            });

            modelBuilder.Entity<TblCliente>(entity =>
            {
                entity.ToTable("tblCliente");

                entity.HasIndex(e => e.Rnc, "UQ__tblClien__CAFF69509FA9FC1C")
                    .IsUnique();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Rnc)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("RNC")
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdGrupoNavigation)
                    .WithMany(p => p.TblClientes)
                    .HasForeignKey(d => d.IdGrupo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblClient__IdGru__2A4B4B5E");

                entity.HasOne(d => d.IdPaisNavigation)
                    .WithMany(p => p.TblClientes)
                    .HasForeignKey(d => d.IdPais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblClient__IdPai__29572725");
            });

            modelBuilder.Entity<TblClienteArticulo>(entity =>
            {
                entity.ToTable("tblClienteArticulo");

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.TblClienteArticulos)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblClient__IdArt__30F848ED");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.TblClienteArticulos)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblClient__IdArt__300424B4");
            });

            modelBuilder.Entity<TblGrupo>(entity =>
            {
                entity.ToTable("tblGrupo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblPai>(entity =>
            {
                entity.ToTable("tblPais");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
