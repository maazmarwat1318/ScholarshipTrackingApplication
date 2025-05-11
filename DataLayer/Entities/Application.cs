using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class Application
{
    public int ApplicationId { get; set; }

    public DateOnly ApplicationDate { get; set; }

    public int? StudentId { get; set; }

    public int? OfferingId { get; set; }

    public virtual ICollection<ApplicationStatusHistory> ApplicationStatusHistories { get; set; } = new List<ApplicationStatusHistory>();

    public virtual ScholarshipOffering? Offering { get; set; }

    public virtual Student? Student { get; set; }
}
