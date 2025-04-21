using System;
using System.Collections.Generic;

namespace StaffQualificationAssessment.Models;

public partial class Criterion
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Metric> Metrics { get; set; } = new List<Metric>();
}
