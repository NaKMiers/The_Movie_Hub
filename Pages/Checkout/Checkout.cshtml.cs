using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Pages.Checkout
{
  public class TicketTypeCheckout
  {
    public string id;
    public string label;
    public string price;
    public int quantity;
  }

  public class SeatCheckout
  {
    public string id;
    public string row;
    public string column;
    public string seatType;
  }

  public class ShowTime
  {
    public string date;
    public string time;
    public string id;
  }

  public class CheckoutModel(TheMovieHubDbContext db) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;

    // Methods
    public IActionResult OnGetCheckout(string fullName, string phone, string email, string movieId, string roomId, string showTimeId, string theaterId, string ticketTypes, string seats, string showTime, float total, string paymentMethod)
    {
      Console.WriteLine(fullName);
      Console.WriteLine(phone);
      Console.WriteLine(email);
      Console.WriteLine(movieId);
      Console.WriteLine(roomId);
      Console.WriteLine(showTimeId);
      Console.WriteLine(theaterId);
      Console.WriteLine(total);
      Console.WriteLine(paymentMethod);
      var tickTypesCheckout = JsonConvert.DeserializeObject<List<TicketTypeCheckout>>(ticketTypes);
      var seatsCheckout = JsonConvert.DeserializeObject<List<SeatCheckout>>(seats);
      var showTimeCheckout = JsonConvert.DeserializeObject<ShowTime>(showTime);

      if (showTimeCheckout == null)
      {
        return new JsonResult(new { message = "Show time is required" });
      }


      List<TicketTypeCheckout> ticketTypesCheckout2 = [];
      int numberOfTickets = 0;
      foreach (var item in tickTypesCheckout)
      {
        for (int i = 0; i < item.quantity; i++)
        {
          ticketTypesCheckout2.Add(item);
          numberOfTickets++;
        }
      }

      for (int i = 0; i < ticketTypesCheckout2.Count; i++)
      {
        var ticketType = ticketTypesCheckout2[i];
        var seat = seatsCheckout[i];

        var newTicket = new Models.Main.Ticket
        {
          Id = Guid.NewGuid(),
          Email = email,
          MovieId = Guid.Parse(movieId),
          TheaterId = Guid.Parse(theaterId),
          TicketTypeId = Guid.Parse(ticketType.id),
          RoomId = Guid.Parse(roomId),
          SeatId = Guid.Parse(seat.id),
          StartAt = DateTime.Parse(showTimeCheckout.date),
          Total = total,
          PaymentMethod = paymentMethod,
          Status = "Pending",
          CreatedAt = DateTime.Now,
        };

        // seat to be booked
        var seatToBeBooked = db.Seats.FirstOrDefault(s => s.Id.ToString() == seat.id);
        if (seatToBeBooked != null)
        {
          seatToBeBooked.IsAvailable = false;
        }

        db.Tickets.Add(newTicket);
      }

      db.SaveChanges();

      return new JsonResult(new { message = "Successfully" });
    }
  }
}