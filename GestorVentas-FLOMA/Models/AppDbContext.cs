using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestorVentas_FLOMA.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Descuento> Descuentos { get; set; }

    public virtual DbSet<Detalleproducto> Detalleproductos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Vendedor> Vendedors { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Database=floma;User=root;Password=floma321;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cliente");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Celular)
                .HasMaxLength(45)
                .HasColumnName("celular");
            entity.Property(e => e.Direccion)
                .HasMaxLength(45)
                .HasColumnName("direccion");
            entity.Property(e => e.Fuente)
                .HasMaxLength(45)
                .HasColumnName("fuente");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Descuento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("descuento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(45)
                .HasColumnName("descripcion");
            entity.Property(e => e.Porcentaje)
                .HasPrecision(10)
                .HasColumnName("porcentaje");
        });

        modelBuilder.Entity<Detalleproducto>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PRIMARY");

            entity.ToTable("detalleproducto");

            entity.HasIndex(e => e.IdProducto, "idProducto_idx");

            entity.Property(e => e.IdDetalle).HasColumnName("idDetalle");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)")
                .HasColumnName("active");
            entity.Property(e => e.Costo)
                .HasPrecision(10)
                .HasColumnName("costo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(45)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.Img)
                .HasColumnType("blob")
                .HasColumnName("img");
            entity.Property(e => e.PrecioVenta).HasColumnName("precioVenta");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Detalleproductos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idProducto");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PRIMARY");

            entity.ToTable("producto");

            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Vendedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("vendedor");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(45)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("venta");

            entity.HasIndex(e => e.IdCliente, "idCliente_idx");

            entity.HasIndex(e => e.IdDescuento, "idDescuento_idx");

            entity.HasIndex(e => e.IdDetalleProducto, "idDetalleProducto_idx");

            entity.HasIndex(e => e.IdVendedor, "idVendedor_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("b'1'")
                .HasColumnType("bit(1)")
                .HasColumnName("active");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Complete)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)")
                .HasColumnName("complete");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(45)
                .HasColumnName("descripcion");
            entity.Property(e => e.DomicilioEntrega)
                .HasMaxLength(45)
                .HasColumnName("domicilioEntrega");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.FechaEntrega)
                .HasColumnType("datetime")
                .HasColumnName("fechaEntrega");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdDescuento).HasColumnName("idDescuento");
            entity.Property(e => e.IdDetalleProducto).HasColumnName("idDetalleProducto");
            entity.Property(e => e.IdVendedor).HasColumnName("idVendedor");
            entity.Property(e => e.Precio).HasColumnName("precio");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("idCliente");

            entity.HasOne(d => d.IdDescuentoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdDescuento)
                .HasConstraintName("idDescuento");

            entity.HasOne(d => d.IdDetalleProductoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdDetalleProducto)
                .HasConstraintName("idDetalleProducto");

            entity.HasOne(d => d.IdVendedorNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdVendedor)
                .HasConstraintName("idVendedor");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
