using OrganizationType =  Diadoc.Api.DataXml.OrganizationType;
using FunctionType = Diadoc.Api.DataXml.Utd820.Hyphens.UniversalTransferDocumentWithHyphensFunction;

namespace Integrations.Diadoc.Domain.DTOs
{
    public class DataForUniversalTransferDocument
    {
        public DateTime DocumentDate { get; set; }
        public string DocumentNumber { get; set; }
        public string Title { get; set; }
        public FunctionType Function { get; set; }
        public string ContractNumber { get; set; } // BaseDocumentName
        public DateTime ContractDate { get; set; } // BaseDocumentDate
        public string AgentContractNumber { get; set; } // F@ ZARA

        public string ClientOrganizationName { get; set; }
        public string ClientInn { get; init; } = string.Empty;
        public string ClientKpp { get; init; } = string.Empty;
        public bool ServiceCode { get; set; } = false;

        /*
        public DateTime FromDate { get; set; }
        public string JurCityName { get; set; }
        public int JurRegionCode { get; set; }
        public string JurRegionName { get; set; }
        public string JurAddress { get; set; }
        public string PhisPostCode { get; set; }
        public string PhisCityName { get; set; }
        public string PhisRegionName { get; set; }
        public string PhisAddress { get; set; }
        */

        public OrganizationType OrganizationType { get; set; }
        public string BoxToId { get; set; }

        public string ClientInnKpp
        {
            get => innkpp;
            init
            {
                innkpp = value.Trim();

                if (!string.IsNullOrEmpty(innkpp))
                {
                    if (innkpp.Length == 12)
                    {
                        this.ClientInn = innkpp;
                        this.OrganizationType = OrganizationType.IndividualEntity;
                    }
                    else
                    {
                        this.ClientInn = this.innkpp.Substring(0, 10);
                        this.ClientKpp = this.innkpp.Substring(11);
                        this.OrganizationType = OrganizationType.LegalEntity;
                    }
                }
            }
        }

        public int Upd
        {
            get { return upd; }
            set
            {
                upd = value;
                Function = FunctionType.СЧФ;
                Title = "Счет-фактура";
                if (upd == 1)
                {
                    Function = FunctionType.СЧФДОП;
                    Title = "УПД";
                }
            }
        }

        public IEnumerable<DocumentTableItem> TableItems { get; set; }

        private string innkpp;
        private int upd;
    }
}
