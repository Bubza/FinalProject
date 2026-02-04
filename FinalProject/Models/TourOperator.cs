using System;
using System.Collections.Generic;

namespace FinalProject.Models;

public partial class TourOperator
{
    public int OperatorId { get; set; }

    public int UserId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? Description { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Route> Routes { get; set; } = new List<Route>();

    public virtual User User { get; set; } = null!;
}
