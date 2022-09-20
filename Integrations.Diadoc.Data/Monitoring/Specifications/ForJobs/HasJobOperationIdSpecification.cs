﻿using System;
using System.Linq.Expressions;
using Data.Models.MonitoringContext;
using Integrations.Diadoc.Domain.Models.Enums;
using SpeciVacation;

namespace Integrations.Diadoc.Data.Monitoring.Specifications.ForJobs
{
    public class HasJobOperationIdSpecification : Specification<Jobs>
    {
        private OperationId OperationId { get; set; }

        public HasJobOperationIdSpecification(OperationId operationId)
        {
            this.OperationId = operationId;
        }

        public override Expression<Func<Jobs, bool>> ToExpression()
        {
            return j => j.OperationId == this.OperationId;
        }
    }
}
