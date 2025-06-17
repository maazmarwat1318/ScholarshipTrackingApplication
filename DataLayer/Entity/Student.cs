

namespace DataLayer.Entity;

public partial class Student
{
    public int Id { get; set; }

    public int? DegreeId { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual Degree? Degree { get; set; }

    public virtual User StudentNavigation { get; set; } = null!;
}
