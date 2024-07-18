using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using the_movie_hub.Models.Main;
using the_movie_hub.Models.ViewModels;
using the_movie_hub.Services;

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

  public class CheckoutModel(TheMovieHubDbContext db, IVnPayService vnPayService) : PageModel
  {
    // Database context
    private readonly TheMovieHubDbContext db = db;
    private readonly IVnPayService _vnPayService = vnPayService;

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

      string url = "";
      if (paymentMethod == "vn-pay")
      {
        var vnPayModel = new VnPaymentRequestModel
        {
          Amount = total,
          CreatedDate = DateTime.Now,
          Description = $"{email} - {phone}",
          FullName = fullName,
          OrderId = Guid.NewGuid().ToString(),
        };

        url = _vnPayService.CreatePaymentUrl(HttpContext, vnPayModel);
      }

      db.SaveChanges();

      // create random order id int
      var random = new Random();
      var orderId = random.Next(10000, 99999);

      var order = new
      {
        fullName,
        phone,
        email,
        movieId,
        roomId,
        showTimeId,
        theaterId,
        total,
        paymentMethod,
        orderId,
      };

      return new JsonResult(new { url, order, message = "Successfully" });
    }

    public IActionResult OnGetPaymentCallBack()
    {
      var response = _vnPayService.PaymentExecute(Request.Query);

      // show response
      Console.WriteLine("response:" + response.VnPayResponseCode);

      if (response == null || response.VnPayResponseCode != "00")
      {
        TempData["FailMessage"] = "Thanh toán thất bại";
        return Page();
      }

      TempData["SuccessMessage"] = "Đặt vé thành công";
      return Page();
    }
  }
}