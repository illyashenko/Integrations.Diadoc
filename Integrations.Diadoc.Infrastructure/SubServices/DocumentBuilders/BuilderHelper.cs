using Hyphens =  Diadoc.Api.DataXml.Utd820.Hyphens;
using Diadoc.Api.DataXml;
using Integrations.Diadoc.Infrastructure.DTOs;
using Integrations.Diadoc.Infrastructure.Settings;

namespace Integrations.Diadoc.Infrastructure.SubServices.DocumentBuilders;

public class BuilderHelper
{
      public static Hyphens.ExtendedOrganizationInfoWithHyphens[] GetSellers(params string[] boxId)
        {
            var sellers = new List<Hyphens.ExtendedOrganizationInfoWithHyphens>();

            foreach (var id in boxId)
            {
                var seller = new Hyphens.ExtendedOrganizationInfoWithHyphens()
                {
                    Item = new Hyphens.ExtendedOrganizationReference()
                    {
                        BoxId = id
                    }
                };

                sellers.Add(seller);
            }

            return sellers.ToArray();
        }

        public static object[] GetSigners(string boxId, string certificate)
        {
            return new object[]
            {
                new SignerReference()
                {
                    BoxId = boxId,
                    CertificateThumbprint = certificate
                }
            };
        }

        public static Hyphens.UniversalTransferDocumentWithHyphensShipper[] GetShippers()
        {
            return new Hyphens.UniversalTransferDocumentWithHyphensShipper[]
            {
                new Hyphens.UniversalTransferDocumentWithHyphensShipper
                {
                    SameAsSeller = 0
                }
            };
        }

        public static Hyphens.Employee GetEmployee(EmployeeSettings employeeSettings)
        {
            return new Hyphens.Employee
            {
                Position = employeeSettings.JobTitle,
                FirstName = employeeSettings.FirstName,
                LastName = employeeSettings.Surname,
                MiddleName = employeeSettings.Patronymic
            };
        }

        public static Hyphens.TransferBase820[] GetTransferBases(DataForUniversalTransferDocument dataForUtd)
        {
            return new Hyphens.TransferBase820[]
            {
                new Hyphens.TransferBase820()
                {
                    BaseDocumentName = DiadocNameConstants.BaseDocumentName,
                    BaseDocumentNumber = (dataForUtd.ServiceCode && AgentContractZara(dataForUtd.TableItems)) ? dataForUtd.AgentContractNumber : dataForUtd.ContractNumber,
                    BaseDocumentDate = dataForUtd.ContractDate.ToString("dd.MM.yyyy")
                }
            };
        }

        public static Hyphens.ExtendedOrganizationInfoWithHyphens[] GetBayer(DataForUniversalTransferDocument dataForUtd)
        {
            return new Hyphens.ExtendedOrganizationInfoWithHyphens[]
            {
                new Hyphens.ExtendedOrganizationInfoWithHyphens
                {
                    Item = new Hyphens.ExtendedOrganizationReference
                    {
                        BoxId = dataForUtd.BoxToId,
                        OrgType = dataForUtd.OrganizationType
                    }
                }
            };
        }

        public static Hyphens.UniversalTransferDocumentWithHyphensDocumentShipment[] GetShipments(DataForUniversalTransferDocument dataForUtd)
        {
            return new Hyphens.UniversalTransferDocumentWithHyphensDocumentShipment[]
            {
                new Hyphens.UniversalTransferDocumentWithHyphensDocumentShipment()
                {
                    Name = dataForUtd.Title,
                    Number = string.Concat(updateDocumentName(dataForUtd), " №", dataForUtd.DocumentNumber),
                    Date = dataForUtd.DocumentDate.ToString("dd.MM.yyyy")
                }
            };
        }

        public static Hyphens.InvoiceTable GetInvoiceTable(DataForUniversalTransferDocument dataForUtd)
        {
            return new Hyphens.InvoiceTable()
            {
                VatSpecified = true,
                TotalSpecified = true,
                TotalWithVatExcludedSpecified = true,
                Vat = dataForUtd.TableItems.Sum(el => el.Nds),
                Total = dataForUtd.TableItems.Sum(el => el.Total),
                TotalWithVatExcluded = dataForUtd.TableItems.Sum(el => el.Tariff),
                WithoutVat = dataForUtd.TableItems.Sum(el => el.Nds) != 0 ? Hyphens.InvoiceTableWithoutVat.False : Hyphens.InvoiceTableWithoutVat.True,
                Item = ListItems(dataForUtd).ToArray()
            };
        }
        
        private static List<Hyphens.InvoiceTableItem> ListItems(DataForUniversalTransferDocument dataUtd)
        {
            var listReturn = new List<Hyphens.InvoiceTableItem>();

            foreach (var tableItem in dataUtd.TableItems)
            {
                var elementItem = new Hyphens.InvoiceTableItem()
                {
                    Vat = tableItem.Nds,
                    VatSpecified = true,
                    TaxRate = tableItem.VatRate,
                    HyphenUnit = Hyphens.InvoiceTableItemHyphenUnit.True,
                    WithoutVat = tableItem.Nds != 0 ? Hyphens.InvoiceTableItemWithoutVat.False : Hyphens.InvoiceTableItemWithoutVat.True,
                    SubtotalWithVatExcluded = tableItem.Tariff,
                    SubtotalWithVatExcludedSpecified = true,
                    Subtotal = tableItem.Total,
                    SubtotalSpecified = true,
                    Product = tableItem.Product,
                    AdditionalInfos = dataUtd.ServiceCode
                        ? new[]
                        {
                            new Hyphens.AdditionalInfo
                            {
                                Id = "ИД",
                                Value = String.Concat(tableItem.StOwnerId, "-", tableItem.StId)
                            }
                        }
                        : null
                };
                listReturn.Add(elementItem);
            }
            return listReturn;

        }
        
        private static string updateDocumentName(DataForUniversalTransferDocument document)
        {
            var rowsNumber = document.TableItems.Count();

            if (rowsNumber == 0)
                return String.Empty;

            var name = String.Concat(" п/п ", rowsNumber != 1 ? String.Concat("1-", rowsNumber) : rowsNumber.ToString());
            return name;
        }

       // F@ for ZARA
       private static bool AgentContractZara(IEnumerable<DocumentTableItem> utdTableItems)
       {
           if (utdTableItems.Count() == 1)
           {
               var isAvailable = utdTableItems.Any(it => it.Product.Contains(DiadocNameConstants.AgencyFee));

               if (isAvailable)
               {
                   return true;
               }
           }
           return false;
       }
}
