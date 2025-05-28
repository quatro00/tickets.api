using System;
using System.Collections.Generic;

namespace tickets.api.Models.Domain;

public partial class EquipoTrabajoIntegrante
{
    public Guid Id { get; set; }

    public Guid EquipoTrabajoId { get; set; }

    public string UsuarioId { get; set; } = null!;

    public bool Activo { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual EquipoTrabajo EquipoTrabajo { get; set; } = null!;

    public virtual AspNetUser Usuario { get; set; } = null!;
}
