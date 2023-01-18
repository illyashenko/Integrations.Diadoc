using System;
using System.Linq.Expressions;
using Data.Models.MonitoringContext;
using Integrations.Diadoc.Data.Monitoring.Enums;
using Integrations.Diadoc.Data.Monitoring.Specifications.Filters;
using SpeciVacation;

namespace Integrations.Diadoc.Data.Monitoring.Specifications.ForJobs
{
    public static class JobFilterConverter
    {
        public static Expression<Func<Job, bool>> ToExpression(this JobFilter filter)
        {
            ISpecification<Job> spec = Specification<Job>.None;

            if (filter.Status != JobStatus.Default)
            {
                spec = spec.Or(new HasJobStatusSpecification(filter.Status));
            }
            if (filter.ServerId != 0)
            {
                spec = spec.And(new HasJobServerIdSpecification(filter.ServerId));
            }
            if (filter.DateFrom != DateTime.MinValue)
            {
                spec = spec.And(new HasJobDateOfCreateSpecification(filter.DateFrom));
            }
            if (filter.OperationIds is not null && filter.OperationIds.Any())
            {
                spec = spec.And(HasJobOperationIdSpecification.CreateAsOr(filter.OperationIds));
            }

            return spec.ToExpression();
        }
    }
}
