using System;
using System.Collections.Generic;

namespace DataLayer.Entity;

public partial class Department
{
    public int Id { get; set; }
    public string DepartmentTitle { get; set; } = null!;

    public virtual ICollection<Degree> Degrees { get; set; } = new List<Degree>();
}
