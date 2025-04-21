using System;
using System.Collections.Generic;

namespace StaffQualificationAssessment.Models;

public partial class EmployeeMetric
{
    public int Id { get; set; }

    public int StaffId { get; set; }

    public int MetricId { get; set; }

    public virtual Metric Metric { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;
}
