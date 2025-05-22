using System.Linq.Expressions;
using tickets.api.Models.Domain;
using tickets.api.Models.Interfaces;

namespace tickets.api.Models.Specifications
{
    public class AreaSpecification : ISpecification<Area>
    {
        //filtros
        public string? Rol { get; set; }

        public Expression<Func<Area, bool>> Criteria { get; }
        public List<string> IncludeStrings { get; set; }

        public AreaSpecification(FiltroGlobal filtro)
        {
            Criteria = p =>
                (filtro.IncluirInactivos || (p.Activo));
        }
    }
}
