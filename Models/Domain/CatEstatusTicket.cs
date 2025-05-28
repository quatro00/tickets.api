using System;
using System.Collections.Generic;

namespace tickets.api.Models.Domain;

public partial class CatEstatusTicket
{
    public Guid Id { get; set; }

    public int Clave { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
