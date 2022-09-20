using System;
using System.Linq.Expressions;
using Data.Models.MonitoringContext;
using Integrations.Diadoc.Data.Monitoring.Specifications.Filters;
using Integrations.Diadoc.Domain.Models.Enums;
using SpeciVacation;

namespace Integrations.Diadoc.Data.Monitoring.Specifications.ForJobs
{
    public static class JobFilterConverter
    {
        public static Expression<Func<Jobs, bool>> ToExpression(this JobFilter filter)
        {
            ISpecification<Jobs> spec = Specification<Jobs>.None;

            if (filter.Status != JobStatus.Default)
            {
                spec = spec.Or(new HasJobStatusSpecification(filter.Status));
            }
            if (filter.OperationId != OperationId.Default)
            {
                spec = spec.And(new HasJobOperationIdSpecification(filter.OperationId));
            }
            if (filter.ServerId != 0)
            {
                spec = spec.And(new HasJobServerIdSpecification(filter.ServerId));
            }
            if (filter.DateFrom != DateTime.MinValue)
            {
                spec = spec.And(new HasJobDateOfCreateSpecification(filter.DateFrom));
            }

            return spec.ToExpression();
        }
    }
}
