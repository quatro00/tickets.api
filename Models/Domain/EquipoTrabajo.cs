using System;
using System.Collections.Generic;

namespace tickets.api.Models.Domain;

public partial class EquipoTrabajo
{
    public Guid Id { get; set; }

    public Guid OrganizacionId { get; set; }

    public string Nombre { get; set; } = null!;

    public string SupervisorId { get; set; } = null!;

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public Guid UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public Guid? UsuarioModificacion { get; set; }

    public virtual ICollection<EquipoTrabajoIntegrante> EquipoTrabajoIntegrantes { get; set; } = new List<EquipoTrabajoIntegrante>();

    public virtual Organizacion Organizacion { get; set; } = null!;

    public virtual ICollection<RelEquipoTrabajoCategorium> RelEquipoTrabajoCategoria { get; set; } = new List<RelEquipoTrabajoCategorium>();

    public virtual AspNetUser Supervisor { get; set; } = null!;
}
