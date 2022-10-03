using Integrations.Diadoc.Data.Apt;
using Integrations.Diadoc.Data.Apt.Specifications.Filters;
using Integrations.Diadoc.Data.Apt.Specifications.ForDocumentTitle;
using Integrations.Diadoc.Data.Monitoring;
using Integrations.Diadoc.Domain.DTOs;
using Integrations.Diadoc.Domain.Models;
using Integrations.Diadoc.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using VatRate = Diadoc.Api.DataXml.Utd820.Hyphens.TaxRateWithTwentyPercentAndTaxedByAgent;
using Parameters = Microsoft.Data.SqlClient.SqlParameter;

namespace Integrations.Diadoc.Infrastructure.Stores;

public class AptStore
{
    private MonitoringContext monitoring { get; set; }
    private AptContext apt { get; set; }

    public AptStore(MonitoringContext monitoring, AptContext apt)
    {
        this.monitoring = monitoring;
        this.apt = apt;
    }

    public async Task<DataForMessageToPost> GetDataNonformalizedDocument(DocumentTitleFilter filter)
    {
        var dataForMessageToPost = await this.apt.DocumentTitles.Where(filter.ToExpression())
            .Select(dt => new DataForMessageToPost()
            {
                BoxToId = dt.TitleString!.Contracts!.CrmClientsRef!.OrganizationsBoxId!.BoxId,
                AttachmentModel = new NonformalizedAttachmentModel
                {
                    DocumentNumber = dt.DocumentNumber ?? String.Empty,
                    DocumentDate = dt.DocumentDate
                }
            }).FirstOrDefaultAsync();
    
        return dataForMessageToPost;
    }

    public async Task<DataForUniversalTransferDocument> GetDataForMessagePost(DocumentTitleFilter filter)
    {
        var dataQueryUpd = from dt in this.apt.DocumentTitles.Where(filter.ToExpression())
            from ts in this.apt.TitleStrings.Where(titleString => titleString.DocumentTitleId == dt.TitleString.Contracts.DocumentId
                                                                  && titleString.DocumentTitleOwnerId == dt.TitleString.Contracts.DocumentOwnerId
                                                                  && titleString.ParameterType == ParameterTypes.ContractStandartNotify)
            from tsAgent in this.apt.TitleStrings.Where(titleString => titleString.DocumentTitleId == dt.TitleString.Contracts.DocumentId
                                                                       && titleString.DocumentTitleOwnerId == dt.TitleString.Contracts.DocumentOwnerId
                                                                       && titleString.ParameterType == ParameterTypes.NewAgentContract)
            select new DataForUniversalTransferDocument
            {
                DocumentNumber = dt.DocumentNumber,
                DocumentDate = dt.TitleStrings.FirstOrDefault(ts => ts.ParameterType == ParameterTypes.NewBillDt).DateTimeValue ?? DateTime.MinValue,
                Upd = new[] { 2, 3 }.Contains(ts.IntValue ?? 0) ? 1 : 0,
                ClientInnKpp = dt.TitleString.Contracts.ClientsRef.INN,
                BoxToId = dt.TitleString.Contracts.CrmClientsRef.OrganizationsBoxId.BoxId,
                ServiceCode = dt.TitleString.Contracts.CrmClientsRef.OrganizationsBoxId.ServiceCode,
                ContractNumber = dt.TitleString.Contracts.NumberContract,
                AgentContractNumber = tsAgent.StringValue ?? "",
                ContractDate = dt.TitleString.Contracts.CrmContractsRef.ContractDate ?? dt
                    .TitleStrings
                    .FirstOrDefault(ts => ts.ParameterType == ParameterTypes.ContractAgentContractDate)
                    .DateTimeValue.Value,
                TableItems = dt.BillSumsCollection.Where(bs => bs.Tariff > 0).Select(bs => new DocumentTableItem
                {
                    Quantity = bs.Quantity,
                    Tariff = bs.Tariff,
                    Nds = bs.Vat ?? 0,
                    Total = bs.Tariff + bs.Vat ?? 0,
                    HasInvoice = bs.HasInvoice,
                    VatRate = Math.Abs(bs.VatPercent) == 0.2M ? VatRate.TwentyPercent : VatRate.Zero,
                    Measure = bs.Measure == string.Empty ? DiadocNameConstants.Measure : bs.Measure,
                    Product = bs.HasInvoice == 1 ? String.Empty : bs.Description,
                    TypeName = bs.ServiceType.TypeName,
                    TypeNamePrint = bs.ServiceType.TypeNamePrint,
                    StId = bs.ServiceType.Id,
                    StOwnerId = bs.ServiceType.OwnerId,
                    IntValue = dt.TitleStrings.FirstOrDefault(ts => ts.ParameterType == ParameterTypes.NewBillContract).IntValue ?? 0,
                    IntValue1 = dt.TitleStrings.FirstOrDefault(ts => ts.ParameterType == ParameterTypes.NewBillContract).IntValue2 ?? 0
                })
            };
    
        var dataUpd = await dataQueryUpd.FirstOrDefaultAsync();
    
        if (dataUpd != null && dataUpd.TableItems.Count() != 0)
        {
            foreach (var item in dataUpd.TableItems)
            {
                if (item.Product == String.Empty)
                {
                    var serviceTypeNameContract = await GetProductName(item.IntValue, item.IntValue1, item.StId, item.StOwnerId);
                    item.Product = CustomIsEmpty(serviceTypeNameContract,
                        CustomIsEmpty(item.TypeNamePrint, item.TypeName));
                }
    
            }
        }
    
        return dataUpd;
    }

    public async Task<SendingDocument> GetDataForSendingDocument(RequestIdData data)
    {
        var sendingDocument = await this.monitoring.DiadocSendingDocuments
            .Where(doc => doc.RequestId == data.RequestId)
            .Select(p => new SendingDocument()
            {
                DocumentId = p.DocumentId,
                DocumentOwnerId = p.DocumentOwnerId,
                Content = p.Content,
                FileName = p.FileName,
                Bill = p.Bill
            }).FirstOrDefaultAsync();

        return sendingDocument;
    }

    public async Task<string> GetOrganizationId(DocumentTitleFilter filter)
    {
        var orgId = await this.apt.DocumentTitles
            .Where(filter.ToExpression())
            .Select(dt => dt.TitleString!.Contracts!.CrmClientsRef!.OrganizationsBoxId!.BoxId)
            .FirstOrDefaultAsync();
    
        return orgId ?? String.Empty;
    }

    private string CustomIsEmpty(string check, string replace)
    {
        return (check ?? string.Empty) == string.Empty ? replace : check;
    }

    private async Task<string> GetProductName(int cId, int cOwnerId, int stId, int stOwnerId)
    {
        Parameters id = new Parameters("c_id", cId);
        Parameters ownerId = new Parameters("c_owner_id", cOwnerId);
        Parameters st = new Parameters("st_id", stId);
        Parameters stO = new Parameters("st_owner_id", stOwnerId);

        var name = await this.apt.ScalarStrings.FromSqlRaw(DiadocNameConstants.fn_bc_GetServiceTypeNameContract, id, ownerId, st, stO)
            .ToListAsync().ConfigureAwait(false);

        return name.FirstOrDefault()?.Value ?? String.Empty;
    }
}
