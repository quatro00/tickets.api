using System;
using System.Collections.Generic;

namespace tickets.api.Models.Domain;

public partial class TicketHistorialArchivo
{
    public Guid Id { get; set; }

    public Guid TicketHistorialId { get; set; }

    public string Nombre { get; set; } = null!;

    public string RutaPublica { get; set; } = null!;

    public string RutaLocal { get; set; } = null!;

    public bool? Activo { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual TicketHistorial TicketHistorial { get; set; } = null!;
}
