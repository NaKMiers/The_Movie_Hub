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

    public IEnumerable<Models.Main.RoomType> RoomTypes { get; set; } = [];

    [BindProperty]
    public Guid RoomTypeId { get; set; }

    // Methods
    public void OnGet()
    {
      RoomTypes = db.RoomTypes;
    }

    public void OnPost()
    {
      Console.WriteLine("RoomTypeId: " + RoomTypeId);

      // check if room type is selected
      if (RoomTypeId == Guid.Empty)
      {
        ModelState.AddModelError("RoomTypeId", "Please select a room type");
        return;
      }

      var ticketType = new TicketType
      {
        Id = Guid.NewGuid(),
        Label = Label,
        Price = Price,
        Description = Description,
        RoomTypeId = RoomTypeId
      };

      db.TicketTypes.Add(ticketType);
      db.SaveChanges();

      Response.Redirect("/Admin/TicketType/Add");
      return;
    }
  }
}