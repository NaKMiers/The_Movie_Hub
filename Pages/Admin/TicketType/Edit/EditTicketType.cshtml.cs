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

    [BindProperty]
    public Guid RoomTypeId { get; set; }

    public IEnumerable<Models.Main.RoomType> RoomTypes { get; set; } = [];

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

      // get room types
      RoomTypes = db.RoomTypes;

      // get the properties
      Label = ticketType.Label;
      Price = ticketType.Price;
      Description = ticketType.Description;
      RoomTypeId = ticketType.RoomTypeId;
    }

    public void OnPost()
    {
      // check if room type is selected
      if (RoomTypeId == Guid.Empty)
      {
        ModelState.AddModelError("RoomTypeId", "Please select a room type");
        return;
      }

      var updatedTicketType = db.TicketTypes.FirstOrDefault(item => item.Id.ToString() == Id);

      if (updatedTicketType == null)
      {
        Response.Redirect("/Admin/TicketType");
        return;
      }

      updatedTicketType.Label = Label;
      updatedTicketType.Price = Price;
      updatedTicketType.Description = Description;
      updatedTicketType.RoomTypeId = RoomTypeId;

      db.SaveChanges();

      Response.Redirect("/Admin/TicketType");
      return;
    }
  }
}