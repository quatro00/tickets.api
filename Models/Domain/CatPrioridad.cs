using System;
using System.Collections.Generic;

namespace tickets.api.Models.Domain;

public partial class CatPrioridad
{
    public Guid Id { get; set; }

    public int Valor { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string? UsuarioCreacionId { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacionId { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
