using System.ComponentModel.DataAnnotations.Schema;
namespace DataLayer.Entity;

[Table("scholarshipoffering")]
public partial class ScholarshipOffering
{
    public int OfferingId { get; set; }

    public DateOnly ApplicationsStartDate { get; set; }

    public DateOnly? ApplicationsEndDate { get; set; }

    public string? Status { get; set; }

    public int? ScholarshipId { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual Scholarship? Scholarship { get; set; }

    public virtual ICollection<Degree> DegreeTitles { get; set; } = new List<Degree>();

    public virtual ICollection<ScholarshipModerator> Moderators { get; set; } = new List<ScholarshipModerator>();

    public virtual ICollection<AcademicTerm> Terms { get; set; } = new List<AcademicTerm>();
}
