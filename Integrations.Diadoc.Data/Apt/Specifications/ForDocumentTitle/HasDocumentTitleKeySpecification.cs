using System.Linq.Expressions;
using Integrations.Diadoc.Data.Apt.Entities;
using SpeciVacation;
using PickPointKey = PickPoint.Models.PickPointKey;

namespace Integrations.Diadoc.Data.Apt.Specifications.ForDocumentTitle
{
    public class HasDocumentTitleKeySpecification : Specification<DocumentTitle>
    {
        public PickPointKey Key { get; set; }

        public HasDocumentTitleKeySpecification(PickPointKey key)
        {
            this.Key = key;
        }

        public override Expression<Func<DocumentTitle, bool>> ToExpression()
        {
            return dt => dt.DocumentId == this.Key.Id && dt.DocumentOwnerId == this.Key.OwnerId;
        }
    }
}