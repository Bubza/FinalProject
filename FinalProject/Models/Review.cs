using System;
using System.Collections.Generic;

namespace FinalProject.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int UserId { get; set; }

    public int RouteId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Route Route { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
