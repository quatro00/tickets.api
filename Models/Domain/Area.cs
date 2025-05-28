using System;
using System.Collections.Generic;

namespace tickets.api.Models.Domain;

public partial class Area
{
    public Guid Id { get; set; }

    public Guid? AreaPadreId { get; set; }

    public string Clave { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public Guid OrganizacionId { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public Guid UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public Guid? UsuarioModificacion { get; set; }

    public virtual Area? AreaPadre { get; set; }

    public virtual ICollection<Area> InverseAreaPadre { get; set; } = new List<Area>();

    public virtual Organizacion Organizacion { get; set; } = null!;

    public virtual ICollection<RelAreaResponsable> RelAreaResponsables { get; set; } = new List<RelAreaResponsable>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
