using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Ticket
{
  public class AddTicketTypeModel(TheMovieHubDbContext db) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties
    [BindProperty]
    public required string Label { get; set; }

    [BindProperty]
    public required float Price { get; set; }

    [BindProperty]
    public required string Description { get; set; }

    // Methods
    public void OnPost()
    {
      Console.WriteLine(Label);
      Console.WriteLine(Price);
      Console.WriteLine(Description);

      var ticketType = new TicketType
      {
        Id = Guid.NewGuid(),
        Label = Label,
        Price = Price,
        Description = Description
      };

      db.TicketTypes.Add(ticketType);
      db.SaveChanges();

      Response.Redirect("/Admin/TicketType/Add");
      return;
    }
  }
}