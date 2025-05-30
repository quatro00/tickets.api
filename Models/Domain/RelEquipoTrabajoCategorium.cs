using System;
using System.Collections.Generic;

namespace tickets.api.Models.Domain;

public partial class RelEquipoTrabajoCategorium
{
    public Guid Id { get; set; }

    public Guid EquipoTrabajoId { get; set; }

    public Guid CategoriaId { get; set; }

    public bool Activo { get; set; }

    public virtual CatCategorium Categoria { get; set; } = null!;

    public virtual EquipoTrabajo EquipoTrabajo { get; set; } = null!;
}
