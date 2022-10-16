using Integrations.Diadoc.Domain.Models.Enums;
using PickPoint.Models;

namespace Integrations.Diadoc.Data.Apt.Specifications.Filters;

public class DocumentTitleFilter
{
    public IEnumerable<PickPointKey>? Keys { get; set; }
    public PickPointKey? Key { get; set; }
    public IEnumerable<DocumentTypes>? Types { get; set; }
    public static DocumentTitleFilter GetFilterDocumentReport(PickPointKey key)
    {
        return new DocumentTitleFilter()
        {
            Key = key,
            Types = new[]
            {
                DocumentTypes.AgentReport, 
                DocumentTypes.AgentReportReturn,
                DocumentTypes.NewBill
            }
        };
    }
}
