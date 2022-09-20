using System.Linq.Expressions;
using Integrations.Diadoc.Data.Apt.Entities;
using Integrations.Diadoc.Data.Apt.Specifications.Filters;
using SpeciVacation;

namespace Integrations.Diadoc.Data.Apt.Specifications.ForDocumentTitle
{
    public static class DocumentTitleFilterConverter
    {
        public static Expression<Func<DocumentTitle, bool>> ToExpression(this DocumentTitleFilter filter)
        {
            ISpecification<DocumentTitle> specification = Specification<DocumentTitle>.None;

            if (filter.Key != null)
            {
                specification = specification.Or(new HasDocumentTitleKeySpecification(filter.Key));
            }
            if (filter.Types.Count() != 0)
            {
                specification =
                    specification.And(HasDocumentTitleToDocumentTypeSpecification.AsOr(filter.Types));
            }
            
            return specification.ToExpression();
        }
    }
}