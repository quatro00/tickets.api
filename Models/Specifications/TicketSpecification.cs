using System.Linq.Expressions;
using tickets.api.Models.Domain;
using tickets.api.Models.Interfaces;

namespace tickets.api.Models.Specifications
{
    public class TicketSpecification : ISpecification<Ticket>
    {
        public Expression<Func<Ticket, bool>> Criteria { get; }
        public List<string> IncludeStrings { get; set; }

        public TicketSpecification(FiltroGlobal filtro)
        {
            Criteria = p =>
                (filtro.IncluirInactivos || (p.Activo)) &&
                (filtro.Id == null || filtro.Id == p.Id);
        }
    }
}
