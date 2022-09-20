using System;
using System.Linq.Expressions;
using Data.Models.MonitoringContext;
using SpeciVacation;

namespace Integrations.Diadoc.Data.Monitoring.Specifications.ForJobs
{
    public class HasJobDateOfCreateSpecification : Specification<Jobs>
    {
        private DateTime DateFrom { get; set; }

        public HasJobDateOfCreateSpecification(DateTime dateFrom)
        {
            this.DateFrom = dateFrom;
        }
        
        public override Expression<Func<Jobs, bool>> ToExpression()
        {
            return j => j.CreateDate > DateFrom;
        }
    }
}
