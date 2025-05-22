using System.Linq.Expressions;
using tickets.api.Models.Interfaces;

namespace tickets.api.Models.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; }
        public List<string> IncludeStrings { get; set; }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
    }
}
