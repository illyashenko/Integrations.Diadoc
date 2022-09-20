using System;
using System.Linq.Expressions;
using Data.Models.MonitoringContext;
using Integrations.Diadoc.Domain.Models.Enums;
using SpeciVacation;

namespace Integrations.Diadoc.Data.Monitoring.Specifications.ForJobs
{
    public class HasJobServerIdSpecification : Specification<Jobs>
    {
        private ServerId ServerId { get; set; }

        public HasJobServerIdSpecification(ServerId serverId) 
        {
            this.ServerId = serverId;
        }

        public override Expression<Func<Jobs, bool>> ToExpression()
        {
            return j => j.ServerId == this.ServerId;
        }
    }
}
