
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entity;

[Table("academicterm")]
public partial class AcademicTerm
{
    public int TermId { get; set; }

    public decimal Year { get; set; }

    public string? TermName { get; set; }

    public virtual ICollection<ScholarshipOffering> Offerings { get; set; } = new List<ScholarshipOffering>();
}
