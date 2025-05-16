using System;
using System.Collections.Generic;

namespace DataLayer.Entity;

public partial class Student
{
    public int StudentId { get; set; }

    public string? DegreeTitle { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual Degree? DegreeTitleNavigation { get; set; }

    public virtual User StudentNavigation { get; set; } = null!;
}
