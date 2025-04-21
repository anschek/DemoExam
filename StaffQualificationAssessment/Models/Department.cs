using System;
using System.Collections.Generic;

namespace StaffQualificationAssessment.Models;

public partial class Department
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
