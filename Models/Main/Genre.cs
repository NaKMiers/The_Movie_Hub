using System;
using System.Collections.Generic;

namespace the_movie_hub.Models.Main;

public partial class Genre
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public virtual ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
}
