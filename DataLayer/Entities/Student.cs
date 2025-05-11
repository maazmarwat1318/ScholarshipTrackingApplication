using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class Student
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? DegreeTitle { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual Degree? DegreeTitleNavigation { get; set; }
}
