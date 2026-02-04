using System;
using System.Collections.Generic;

namespace FinalProject.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int UserId { get; set; }

    public int RouteId { get; set; }

    public DateTime? BookingDate { get; set; }

    public string? Status { get; set; }

    public int? PeopleCount { get; set; }

    public virtual Route Route { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
