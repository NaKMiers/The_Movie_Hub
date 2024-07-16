using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Account.AccountHistory
{
    public class AccountHistoryModel(TheMovieHubDbContext db, IWebHostEnvironment environment) : PageModel
    {
        // Database context
        private readonly TheMovieHubDbContext db = db;
        private readonly IWebHostEnvironment environment = environment;

        // Properties
        [BindProperty]
        public IFormFile? Avatar { get; set; }

        public IEnumerable<Models.Main.Ticket> Tickets { get; set; } = [];

        // Methods
        public void OnGet()
        {
            var session = HttpContext.Session.GetString("User");
            if (session == null)
            {
                Response.Redirect("/Account/Login");
            }

            var user = session != null ? JsonConvert.DeserializeObject<User>(session) : null;
            if (user == null)
            {
                Response.Redirect("/Account/Login");
            }

            // get all tickets include movie, theater
            Tickets = db.Tickets.Where(t => t.Email == user.Email)
                .Include(t => t.Movie)
                .Include(t => t.Theater)
                .Include(t => t.TicketType)
                .Include(t => t.Room)
                .Include(t => t.Seat);
        }

    }
}