using MassTransitRMQExtensions.Attributes.JobAttributes;

namespace Integrations.Diadoc.Service.Controllers;

public class JobController
{
    [RunJob("1/5 * * * * ?")]
    public void ProcessJobs()
    {
        Console.WriteLine("Test!!");
    }
}
