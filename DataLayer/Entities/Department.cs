using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class Department
{
    public string DepartmentTitle { get; set; } = null!;

    public virtual ICollection<Degree> Degrees { get; set; } = new List<Degree>();
}
