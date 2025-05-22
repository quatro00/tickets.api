using System;
using System.Collections.Generic;

namespace tickets.api.Models.Domain;

public partial class RelAreaResponsable
{
    public Guid Id { get; set; }

    public Guid AreaId { get; set; }

    public string UsuarioId { get; set; } = null!;

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual Area Area { get; set; } = null!;

    public virtual AspNetUser Usuario { get; set; } = null!;
}
