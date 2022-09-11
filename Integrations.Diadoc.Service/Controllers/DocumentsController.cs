using MassTransitRMQExtensions.Attributes.JobAttributes;

namespace Integrations.Diadoc.Service.Controllers;

public class DocumentsController
{
    [RunJob("1/5 * * * * ?")]
    public void Start()
    {
        Console.WriteLine("Test!!");
    }
}
