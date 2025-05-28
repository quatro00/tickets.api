using System.Linq.Expressions;
using tickets.api.Models.Domain;
using tickets.api.Models.Interfaces;

namespace tickets.api.Models.Specifications
{
    public class EquipoTrabajoSpecification : ISpecification<EquipoTrabajo>
    {
        public Expression<Func<EquipoTrabajo, bool>> Criteria { get; }
        public List<string> IncludeStrings { get; set; }

        public EquipoTrabajoSpecification(FiltroGlobal filtro)
        {
            Criteria = p =>
                (filtro.IncluirInactivos || (p.Activo));
        }
    }
}
