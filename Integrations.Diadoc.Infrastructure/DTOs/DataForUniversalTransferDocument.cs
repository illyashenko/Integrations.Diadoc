using System.Text.RegularExpressions;
using OrganizationType =  Diadoc.Api.DataXml.OrganizationType;
using FunctionType = Diadoc.Api.DataXml.Utd820.Hyphens.UniversalTransferDocumentWithHyphensFunction;

namespace Integrations.Diadoc.Infrastructure.DTOs
{
    public class DataForUniversalTransferDocument
    {
        public DateTime DocumentDate { get; set; }
        public string? DocumentNumber { get; set; }
        public string? Title { get; set; }
        public FunctionType Function { get; set; }
        public string? ContractNumber { get; set; }
        public DateTime ContractDate { get; set; }
        public string? AgentContractNumber { get; set; }

        public string? ClientOrganizationName { get; set; }
        public string ClientInn { get; init; } = string.Empty;
        public string ClientKpp { get; init; } = string.Empty;
        public bool? ServiceCode { get; set; } = false;
        public OrganizationType OrganizationType { get; set; }
        public string? BoxToId { get; set; }

        public string? ClientInnKpp
        {
            get => _innKpp;
            init
            {
                _innKpp = value?.Trim();

                if (!string.IsNullOrEmpty(_innKpp))
                {
                    if (_innKpp.Length == 12)
                    {
                        this.ClientInn = _innKpp;
                        this.OrganizationType = OrganizationType.IndividualEntity;
                    }
                    else if (Regex.IsMatch(_innKpp, @"([0-9]{10})/([0-9]{9})")) 
                    {
                        this.ClientInn = this._innKpp.Substring(0, 10);
                        this.ClientKpp = this._innKpp.Substring(11);
                        this.OrganizationType = OrganizationType.LegalEntity;
                    }
                }
            }
        }

        public int Upd
        {
            get => _upd;
            set
            {
                _upd = value;
                Function = FunctionType.СЧФ;
                Title = "Счет-фактура";
                if (_upd == 1)
                {
                    Function = FunctionType.СЧФДОП;
                    Title = "УПД";
                }
            }
        }

        public IEnumerable<DocumentTableItem>? TableItems { get; set; }

        public bool ValidateBoxId()
        {
            return !String.IsNullOrEmpty(this.BoxToId);
        }
        
         public bool ValidateInnKpp()
        {
            if (string.IsNullOrEmpty(this.ClientInn))
            {
                return false;
            }
            
            if (this.ClientInn.Length == 12)
            {
                return true;
            }

            if (this.ClientInn.Length == 10 && this.ClientKpp.Length == 9)
            {
                return true;
            }

            return false;
        }

        private string? _innKpp;
        private int _upd;
    }
}
