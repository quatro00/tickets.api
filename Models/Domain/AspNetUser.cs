using System;
using System.Collections.Generic;

namespace tickets.api.Models.Domain;

public partial class AspNetUser
{
    public string Id { get; set; } = null!;

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool? EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? PhoneNumberConfirmed { get; set; }

    public bool? TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool? LockoutEnabled { get; set; }

    public int? AccessFailedCount { get; set; }

    public string? Nombre { get; set; }

    public string? Apellidos { get; set; }

    public Guid? OrganizacionId { get; set; }

    public string? Avatar { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual ICollection<EquipoTrabajoIntegrante> EquipoTrabajoIntegrantes { get; set; } = new List<EquipoTrabajoIntegrante>();

    public virtual ICollection<EquipoTrabajo> EquipoTrabajos { get; set; } = new List<EquipoTrabajo>();

    public virtual Organizacion? Organizacion { get; set; }

    public virtual ICollection<RelAreaResponsable> RelAreaResponsables { get; set; } = new List<RelAreaResponsable>();

    public virtual ICollection<TicketHistorial> TicketHistorials { get; set; } = new List<TicketHistorial>();

    public virtual ICollection<Ticket> TicketUsuarioAsignados { get; set; } = new List<Ticket>();

    public virtual ICollection<Ticket> TicketUsuarioCreacions { get; set; } = new List<Ticket>();

    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}
