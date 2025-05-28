using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using tickets.api.Models.Domain;

namespace tickets.api.Data;

public partial class DbAb1c8aTicketsContext : DbContext
{
    public DbAb1c8aTicketsContext()
    {
    }

    public DbAb1c8aTicketsContext(DbContextOptions<DbAb1c8aTicketsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetSystem> AspNetSystems { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<CatCategorium> CatCategoria { get; set; }

    public virtual DbSet<CatEstatusTicket> CatEstatusTickets { get; set; }

    public virtual DbSet<CatPrioridad> CatPrioridads { get; set; }

    public virtual DbSet<Configuracion> Configuracions { get; set; }

    public virtual DbSet<EquipoTrabajo> EquipoTrabajos { get; set; }

    public virtual DbSet<EquipoTrabajoIntegrante> EquipoTrabajoIntegrantes { get; set; }

    public virtual DbSet<Organizacion> Organizacions { get; set; }

    public virtual DbSet<RelAreaResponsable> RelAreaResponsables { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketArchivo> TicketArchivos { get; set; }

    public virtual DbSet<TicketHistorial> TicketHistorials { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=sql1003.site4now.net;Initial Catalog=db_ab1c8a_tickets;Persist Security Info=True;User ID=db_ab1c8a_tickets_admin;Password=Suikoden2;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.ToTable("Area");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Clave).HasMaxLength(50);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(250);
            entity.Property(e => e.Telefono).HasMaxLength(250);

            entity.HasOne(d => d.AreaPadre).WithMany(p => p.InverseAreaPadre)
                .HasForeignKey(d => d.AreaPadreId)
                .HasConstraintName("FK_Area_Area");

            entity.HasOne(d => d.Organizacion).WithMany(p => p.Areas)
                .HasForeignKey(d => d.OrganizacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Area_Organizacion");
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);

            entity.HasOne(d => d.Sistema).WithMany(p => p.AspNetRoles)
                .HasForeignKey(d => d.SistemaId)
                .HasConstraintName("FK_AspNetRoles_AspNetSystem");
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.Property(e => e.RoleId).HasMaxLength(450);

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetSystem>(entity =>
        {
            entity.ToTable("AspNetSystem");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Clave).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(500);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.Property(e => e.Apellidos).HasMaxLength(250);
            entity.Property(e => e.Avatar).HasMaxLength(450);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Nombre).HasMaxLength(250);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasOne(d => d.Organizacion).WithMany(p => p.AspNetUsers)
                .HasForeignKey(d => d.OrganizacionId)
                .HasConstraintName("FK_AspNetUsers_Organizacion");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<CatCategorium>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.UsuarioCreacionId).HasMaxLength(450);
            entity.Property(e => e.UsuarioModificacionId).HasMaxLength(450);
        });

        modelBuilder.Entity<CatEstatusTicket>(entity =>
        {
            entity.ToTable("CatEstatusTicket");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Descripcion).HasMaxLength(520);
            entity.Property(e => e.Nombre).HasMaxLength(520);
        });

        modelBuilder.Entity<CatPrioridad>(entity =>
        {
            entity.ToTable("CatPrioridad");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.UsuarioCreacionId).HasMaxLength(450);
            entity.Property(e => e.UsuarioModificacionId).HasMaxLength(450);
        });

        modelBuilder.Entity<Configuracion>(entity =>
        {
            entity.ToTable("Configuracion");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Codigo).HasMaxLength(500);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Modulo).HasMaxLength(50);
            entity.Property(e => e.ValorDate).HasColumnType("datetime");
            entity.Property(e => e.ValorDecimal).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.ValorString).HasMaxLength(500);
        });

        modelBuilder.Entity<EquipoTrabajo>(entity =>
        {
            entity.ToTable("EquipoTrabajo");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.SupervisorId).HasMaxLength(450);

            entity.HasOne(d => d.Organizacion).WithMany(p => p.EquipoTrabajos)
                .HasForeignKey(d => d.OrganizacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EquipoTrabajo_Organizacion");

            entity.HasOne(d => d.Supervisor).WithMany(p => p.EquipoTrabajos)
                .HasForeignKey(d => d.SupervisorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EquipoTrabajo_AspNetUsers");
        });

        modelBuilder.Entity<EquipoTrabajoIntegrante>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioId).HasMaxLength(450);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.EquipoTrabajo).WithMany(p => p.EquipoTrabajoIntegrantes)
                .HasForeignKey(d => d.EquipoTrabajoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EquipoTrabajoIntegrantes_EquipoTrabajo");

            entity.HasOne(d => d.Usuario).WithMany(p => p.EquipoTrabajoIntegrantes)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EquipoTrabajoIntegrantes_AspNetUsers");
        });

        modelBuilder.Entity<Organizacion>(entity =>
        {
            entity.ToTable("Organizacion");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Clave).HasMaxLength(500);
            entity.Property(e => e.Direccion).HasMaxLength(500);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(500);
            entity.Property(e => e.Responsable).HasMaxLength(500);
            entity.Property(e => e.Telefono).HasMaxLength(500);
        });

        modelBuilder.Entity<RelAreaResponsable>(entity =>
        {
            entity.ToTable("RelAreaResponsable");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.UsuarioId).HasMaxLength(450);

            entity.HasOne(d => d.Area).WithMany(p => p.RelAreaResponsables)
                .HasForeignKey(d => d.AreaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RelAreaResponsable_Area");

            entity.HasOne(d => d.Usuario).WithMany(p => p.RelAreaResponsables)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RelAreaResponsable_AspNetUsers");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("Ticket");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.AreaEspecifica).HasMaxLength(500);
            entity.Property(e => e.CorreoContacto).HasMaxLength(500);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.DesdeCuando).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Folio).ValueGeneratedOnAdd();
            entity.Property(e => e.NombreContacto).HasMaxLength(500);
            entity.Property(e => e.TelefonoContacto).HasMaxLength(500);
            entity.Property(e => e.UsuarioAsignadoId).HasMaxLength(450);
            entity.Property(e => e.UsuarioCreacionId).HasMaxLength(450);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(450);

            entity.HasOne(d => d.Area).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.AreaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Area");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_CatCategoria");

            entity.HasOne(d => d.EstatusTicket).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.EstatusTicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_CatEstatusTicket");

            entity.HasOne(d => d.Prioridad).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.PrioridadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_CatPrioridad");

            entity.HasOne(d => d.UsuarioCreacion).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.UsuarioCreacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_AspNetUsers");
        });

        modelBuilder.Entity<TicketArchivo>(entity =>
        {
            entity.ToTable("TicketArchivo");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Url).HasMaxLength(500);

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketArchivos)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TicketArchivo_Ticket");
        });

        modelBuilder.Entity<TicketHistorial>(entity =>
        {
            entity.ToTable("TicketHistorial");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Comentario).HasMaxLength(500);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.UsuarioId).HasMaxLength(450);

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketHistorials)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TicketHistorial_Ticket");

            entity.HasOne(d => d.Usuario).WithMany(p => p.TicketHistorials)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TicketHistorial_AspNetUsers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
