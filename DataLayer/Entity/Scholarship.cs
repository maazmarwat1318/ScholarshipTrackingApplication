
namespace DataLayer.Entity;

public partial class Scholarship
{
    public int ScholarshipId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string QueryEmail { get; set; } = null!;

    public virtual ICollection<ScholarshipOffering> ScholarshipOfferings { get; set; } = new List<ScholarshipOffering>();
}
