using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Account.AccountHistory
{
    public class AccountHistoryModel(TheMovieHubDbContext db) : PageModel
    {
        // Database context
        private readonly TheMovieHubDbContext db = db;

        // Properties
        [BindProperty]
        public IFormFile? Avatar { get; set; }

        public IEnumerable<Models.Main.Ticket> Tickets { get; set; } = [];

        public List<GroupedTickets> GroupedTickets { get; set; } = [];

        public int TicketCount { get; set; } = 0;

        // Methods
        public void OnGet()
        {
            var session = HttpContext.Session.GetString("User");
            if (session == null)
            {
                Response.Redirect("/Account/Login");
                return;
            }

            var user = session != null ? JsonConvert.DeserializeObject<User>(session) : null;
            if (user == null)
            {
                Response.Redirect("/Account/Login");
                return;
            }

            // Get all tickets include movie, theater and group by CreatedAt
            GroupedTickets = [.. db.Tickets
                .Where(t => t.Email == user.Email)
                .Include(t => t.Movie)
                .Include(t => t.Theater)
                .Include(t => t.TicketType)
                .Include(t => t.Room)
                .Include(t => t.Seat)
                .AsEnumerable()
                .GroupBy(t => t.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"))
                .Select(g => new GroupedTickets
                {
                    CreatedAt = DateTime.Parse(g.Key),
                    Tickets = [.. g]
                })
                .OrderByDescending(g => g.CreatedAt)];

            // calc total ticket count
            TicketCount = GroupedTickets.Sum(g => g.Tickets.Count);
        }
    }

    public class GroupedTickets
    {
        public DateTime CreatedAt { get; set; }
        public List<Models.Main.Ticket> Tickets { get; set; } = [];
    }
}