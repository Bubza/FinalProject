using System;
using System.Collections.Generic;

namespace FinalProject.Models;

public partial class Route
{
    public int RouteId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Location { get; set; } = null!;

    public decimal Price { get; set; }

    public int? DurationHours { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? ImageUrl { get; set; }

    public int OperatorId { get; set; }

    public int CategoryId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Category Category { get; set; } = null!;

    public virtual TourOperator Operator { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
