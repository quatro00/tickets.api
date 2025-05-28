using System;
using System.Collections.Generic;

namespace tickets.api.Models.Domain;

public partial class TicketHistorial
{
    public Guid Id { get; set; }

    public Guid TicketId { get; set; }

    public DateTime Fecha { get; set; }

    public string UsuarioId { get; set; } = null!;

    public string Comentario { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;

    public virtual AspNetUser Usuario { get; set; } = null!;
}
