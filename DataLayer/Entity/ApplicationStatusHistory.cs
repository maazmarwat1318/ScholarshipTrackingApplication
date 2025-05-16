using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entity;

[Table("applicationstatushistory")]
public partial class ApplicationStatusHistory
{
    public int HistoryId { get; set; }

    public int? ApplicationId { get; set; }

    public string? Status { get; set; }

    public DateOnly ChangedAt { get; set; }

    public int? ModeratorId { get; set; }

    public virtual Application? Application { get; set; }

    public virtual ScholarshipModerator? Moderator { get; set; }
}
