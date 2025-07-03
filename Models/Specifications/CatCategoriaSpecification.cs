using System.Linq.Expressions;
using tickets.api.Models.Domain;
using tickets.api.Models.Interfaces;

namespace tickets.api.Models.Specifications
{
    public class CatCategoriaSpecification : ISpecification<CatCategorium>
    {

        public Expression<Func<CatCategorium, bool>> Criteria { get; }
        public List<string> IncludeStrings { get; set; }

        public CatCategoriaSpecification(FiltroGlobal filtro)
        {
            Criteria = p =>
                (filtro.IncluirInactivos || (p.Activo));
        }
    }
}
