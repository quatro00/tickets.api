using System;
using System.Collections.Generic;

namespace tickets.api.Models.Domain;

public partial class Ticket
{
    public Guid Id { get; set; }

    public int Folio { get; set; }

    public Guid CategoriaId { get; set; }

    public Guid PrioridadId { get; set; }

    public Guid AreaId { get; set; }

    public string Descripcion { get; set; } = null!;

    public string AreaEspecifica { get; set; } = null!;

    public string? NombreContacto { get; set; }

    public string? TelefonoContacto { get; set; }

    public string? CorreoContacto { get; set; }

    public bool AfectaOperacion { get; set; }

    public DateTime DesdeCuando { get; set; }

    public Guid EstatusTicketId { get; set; }

    public string? UsuarioAsignadoId { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacionId { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual Area Area { get; set; } = null!;

    public virtual CatCategorium Categoria { get; set; } = null!;

    public virtual CatEstatusTicket EstatusTicket { get; set; } = null!;

    public virtual CatPrioridad Prioridad { get; set; } = null!;

    public virtual ICollection<TicketArchivo> TicketArchivos { get; set; } = new List<TicketArchivo>();

    public virtual ICollection<TicketHistorial> TicketHistorials { get; set; } = new List<TicketHistorial>();

    public virtual AspNetUser UsuarioCreacion { get; set; } = null!;
}
