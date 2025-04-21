using System;
using System.Collections.Generic;

namespace StaffQualificationAssessment.Models;

public partial class Staff
{
    public int Id { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public int YearBirthday { get; set; }

    public int CodStaff { get; set; }

    public int? PodstId { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Post? Podst { get; set; }
}
