using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entity;

[Table("scholarshipmoderator")]
public partial class ScholarshipModerator
{
    public int ModeratorId { get; set; }

    public virtual ICollection<ApplicationStatusHistory> ApplicationStatusHistories { get; set; } = new List<ApplicationStatusHistory>();

    public virtual User Moderator { get; set; } = null!;

    public virtual ICollection<ScholarshipOffering> Offerings { get; set; } = new List<ScholarshipOffering>();
}
