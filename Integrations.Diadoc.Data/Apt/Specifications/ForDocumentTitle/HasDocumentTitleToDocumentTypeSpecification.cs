using System.Linq.Expressions;
using Integrations.Diadoc.Data.Apt.Entities;
using Integrations.Diadoc.Domain.Models.Enums;
using SpeciVacation;

namespace Integrations.Diadoc.Data.Apt.Specifications.ForDocumentTitle
{
    public class HasDocumentTitleToDocumentTypeSpecification : Specification<DocumentTitle>
    {
        public DocumentTypes DocumentType { get; set; }

        public HasDocumentTitleToDocumentTypeSpecification(DocumentTypes documentType)
        {
            this.DocumentType = documentType;
        }

        public override Expression<Func<DocumentTitle, bool>> ToExpression()
        {
            return dt => dt.DocumentType == this.DocumentType;
        }
        public static ISpecification<DocumentTitle> AsOr(IEnumerable<DocumentTypes> documentTypes)
        {
            return documentTypes.ToOrSpecification(k => new HasDocumentTitleToDocumentTypeSpecification(k));
        }
    }
}