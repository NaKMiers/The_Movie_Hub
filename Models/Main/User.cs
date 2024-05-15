using System;
using System.Collections.Generic;

namespace the_movie_hub.Models.Main;

public partial class User
{
    public string Id { get; set; } = null!;

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Authtype { get; set; }

    public DateOnly? Birthday { get; set; }

    public double? Expended { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
