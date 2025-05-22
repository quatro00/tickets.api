using System.Linq.Expressions;
using tickets.api.Models.Domain;
using tickets.api.Models.Interfaces;

namespace tickets.api.Models.Specifications
{
    public class AspNetUsersSpecification : ISpecification<AspNetUser>
    {
        //filtros
        public string? Rol { get; set; }

        public Expression<Func<AspNetUser, bool>> Criteria { get; }
        public List<string> IncludeStrings { get; set; }

        public AspNetUsersSpecification(FiltroGlobal filtro)
        {
            Criteria = p =>
                (filtro.IncluirInactivos || (p.Activo ?? false)) &&
                (this.Rol == null || p.Roles.Any(r=>r.Name == this.Rol));
        }
    }
}
