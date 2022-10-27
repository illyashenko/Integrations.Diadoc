using System;
using System.Linq.Expressions;
using Data.Models.MonitoringContext;
using Integrations.Diadoc.Data.Monitoring.Enums;
using SpeciVacation;

namespace Integrations.Diadoc.Data.Monitoring.Specifications.ForJobs
{
    public class HasJobOperationIdSpecification : Specification<Job>
    {
        private OperationId OperationId { get; set; }

        public HasJobOperationIdSpecification(OperationId operationId)
        {
            this.OperationId = operationId;
        }

        public override Expression<Func<Job, bool>> ToExpression()
        {
            return j => j.OperationId == this.OperationId;
        }
    }
}
