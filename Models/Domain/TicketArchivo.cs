using System;
using System.Collections.Generic;

namespace tickets.api.Models.Domain;

public partial class TicketArchivo
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public Guid TicketId { get; set; }

    public DateTime Fecha { get; set; }

    public string Url { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
