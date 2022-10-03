using Function = Diadoc.Api.DataXml.Utd820.Hyphens.UniversalTransferDocumentWithHyphensFunction;

namespace Integrations.Diadoc.Domain.Models;

public class DiadocNameConstants
{
    public static string NonFormalizedDocument => "nonformalized";
    public static string KeyName => "FileName";
    public static string GetDocumentName(Function functionType)
    {
        switch (functionType)
        {
            case Function.СЧФДОП :
                return @"Счет-фактура и документ об отгрузке товаров (выполнении работ), передаче имущественных прав (документ об оказании услуг)";
            case Function.СЧФ :
                return "Счет-фактура";
            default:
                return string.Empty;
        }
    }

    public static string GetTitle(int upd)
    {
        switch (upd)
        {
            case 0 :
                return "Счет-фактура";
            case 1 :
                return "УПД";
            default:
                return String.Empty;
        }
    }

    public static string OperationInfo => @"Товар (груз) передал / услуги, результаты работ, права сдал";

    public static string BaseDocumentName => "Договор логистического обслуживания";

    public static string Currency => "643";

    public static string Measure => "Услуга";
    public static string TypeNameId => "XmlAcceptanceCertificate";

    public static string DefaultName => "default";

    public static string fn_bc_GetServiceTypeNameContract => @"USE APT; SELECT dbo.fn_bc_GetServiceTypeNameContract(@c_id, @c_owner_id, @st_id, @st_owner_id) AS Value";

    public static string AgencyFee => "Агентское вознаграждение";
}
