using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Admin.Ticket
{
  public class EditTicketTypeModel(TheMovieHubDbContext db) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Properties
    [FromRoute]
    public required string Id { get; set; }

    [BindProperty]
    public required string Label { get; set; }

    [BindProperty]
    public required float Price { get; set; }

    [BindProperty]
    public required string Description { get; set; }

    // Methods
    public void OnGet()
    {
      // Get the ticket type
      var ticketType = db.TicketTypes.FirstOrDefault(item => item.Id.ToString() == Id);

      if (ticketType == null)
      {
        Response.Redirect("/Admin/TicketType");
        return;
      }

      // Set the properties
      Label = ticketType.Label;
      Price = ticketType.Price;
      Description = ticketType.Description;
    }

    public void OnPost()
    {
      var updatedTicketType = db.TicketTypes.FirstOrDefault(item => item.Id.ToString() == Id);

      if (updatedTicketType == null)
      {
        Response.Redirect("/Admin/TicketType");
        return;
      }

      updatedTicketType.Label = Label;
      updatedTicketType.Price = Price;
      updatedTicketType.Description = Description;

      db.SaveChanges();

      Response.Redirect("/Admin/TicketType");
      return;
    }
  }
}