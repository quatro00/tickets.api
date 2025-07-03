using System.Linq.Expressions;
using tickets.api.Models.Domain;
using tickets.api.Models.Interfaces;

namespace tickets.api.Models.Specifications
{
    public class OrganizacionSpecification : ISpecification<Organizacion>
    {
        public Guid? OrganizacionId { get; set; }
        public Expression<Func<Organizacion, bool>> Criteria { get; }
        public List<string> IncludeStrings { get; set; }
        public OrganizacionSpecification(FiltroGlobal filtro)
        {
            Criteria = p =>
                (filtro.IncluirInactivos || (p.Activo)) &&
                (OrganizacionId == null || OrganizacionId == p.Id);
        }
    }
}
