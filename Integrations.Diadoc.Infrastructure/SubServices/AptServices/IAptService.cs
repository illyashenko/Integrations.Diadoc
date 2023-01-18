using Diadoc.Api.Proto;

namespace Integrations.Diadoc.Infrastructure.SubServices.AptServices;

public interface IAptService
{
    Task DiadocSaveCounteragent(Counteragent counteragent);
}
