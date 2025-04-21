using System;
using System.Collections.Generic;

namespace StaffQualificationAssessment.Models;

public partial class Metric
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public double? Weight { get; set; }

    public int? CriteriaId { get; set; }

    public virtual Criterion? Criteria { get; set; }
}
