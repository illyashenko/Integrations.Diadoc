using System;
using System.Linq.Expressions;
using Data.Models.MonitoringContext;
using Integrations.Diadoc.Domain.Models.Enums;
using SpeciVacation;

namespace Integrations.Diadoc.Data.Monitoring.Specifications.ForJobs
{
    public class HasJobServerIdSpecification : Specification<Job>
    {
        private int ServerId { get; set; }

        public HasJobServerIdSpecification(int serverId) 
        {
            this.ServerId = serverId;
        }

        public override Expression<Func<Job, bool>> ToExpression()
        {
            return j => j.ServerId == this.ServerId;
        }
    }
}
