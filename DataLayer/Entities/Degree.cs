using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class Degree
{
    public string DegreeTitle { get; set; } = null!;

    public string? DepartmentTitle { get; set; }

    public virtual Department? DepartmentTitleNavigation { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<ScholarshipOffering> Offerings { get; set; } = new List<ScholarshipOffering>();
}
