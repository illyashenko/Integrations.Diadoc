using System;
using System.Linq.Expressions;
using Data.Models.MonitoringContext;
using Integrations.Diadoc.Domain.Models.Enums;
using SpeciVacation;

namespace Integrations.Diadoc.Data.Monitoring.Specifications.ForJobs
{
    public class HasJobStatusSpecification : Specification<Jobs>
    {
        private JobStatus Status { get; set; }

        public HasJobStatusSpecification(JobStatus jobsStatus)
        {
            this.Status = jobsStatus;
        }

        public override Expression<Func<Jobs, bool>> ToExpression()
        {
            return j => j.Status == this.Status;
        }
    }
}
