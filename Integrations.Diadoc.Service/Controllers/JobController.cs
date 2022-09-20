using MassTransitRMQExtensions.Attributes.JobAttributes;

namespace Integrations.Diadoc.Service.Controllers;

public class JobController
{
    [RunJob("1/30 * * * * ?")]
    public async void ProcessJobs()
    {
        Console.WriteLine("!!! TEST !!!");
    }
}
