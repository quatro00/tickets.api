using System.Linq.Expressions;
using tickets.api.Models.Domain;
using tickets.api.Models.Interfaces;

namespace tickets.api.Models.Specifications
{
    public class CatPrioridadSpecification : ISpecification<CatPrioridad>
    {

        public Expression<Func<CatPrioridad, bool>> Criteria { get; }
        public List<string> IncludeStrings { get; set; }

        public CatPrioridadSpecification(FiltroGlobal filtro)
        {
            Criteria = p =>
                (filtro.IncluirInactivos || (p.Activo));
        }
    }
}
