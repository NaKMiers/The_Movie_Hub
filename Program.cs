using the_movie_hub.Models.Main;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TheMovieHubDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("localDB"));
        options.EnableSensitiveDataLogging(false);
    }
);

// config session
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120);
});
JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    Formatting = Formatting.Indented,
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
};

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


// MARK: Auth
// [/login]
app.MapControllerRoute(
    name: "Login",
    pattern: "/login",
    defaults: new { controller = "Auth", action = "Login" }
);

// [/change-password]
app.MapControllerRoute(
    name: "Change Password",
    pattern: "/change-password",
    defaults: new { controller = "Auth", action = "ChangePassword" }
);

// MARK: Showtime
// [/showtimes]
app.MapControllerRoute(
    name: "Showtimes",
    pattern: "/showtimes",
    defaults: new { controller = "Showtime", action = "Index" }
);

// MARK: Movie
// [/movie]
app.MapControllerRoute(
    name: "Movie",
    pattern: "/movie",
    defaults: new { controller = "Movie", action = "Index" }
);

// [/search]
app.MapControllerRoute(
    name: "Search",
    pattern: "/search",
    defaults: new { controller = "Movie", action = "Search" }
);

// [/movie/showing]
app.MapControllerRoute(
    name: "Movie Showing",
    pattern: "/movie/showing",
    defaults: new { controller = "Movie", action = "MovieShowing" }
);

// [/movie/upcoming]
app.MapControllerRoute(
    name: "Movie Upcomming",
    pattern: "/movie/upcoming",
    defaults: new { controller = "Movie", action = "Upcoming" }
);

// [/movie/{id}]
app.MapControllerRoute(
    name: "Movie Detail",
    pattern: "/movie/{id}",
    defaults: new { controller = "Movie", action = "MovieDetail" }
);

// MARK: Account
// [/account/account-profile]
app.MapControllerRoute(
    name: "Account Profile",
    pattern: "/account/account-profile",
    defaults: new { controller = "Account", action = "AccountProfile" }
);

// [/account/account-history]
app.MapControllerRoute(
    name: "Account History",
    pattern: "/account/account-history",
    defaults: new { controller = "Account", action = "AccountHistory" }
);

// MARK: Ticket
// [/book-tickets]
app.MapControllerRoute(
    name: "Book Tickets",
    pattern: "/book-tickets",
    defaults: new { controller = "Ticket", action = "Index" }
);

// [/book-tickets/{id}]
app.MapControllerRoute(
    name: "Book Tickets",
    pattern: "/book-tickets/{id}",
    defaults: new { controller = "Ticket", action = "BookTicket" }
);

// MARK: Checkout
// [/checkout]
app.MapControllerRoute(
    name: "Checkout",
    pattern: "/checkout",
    defaults: new { controller = "Checkout", action = "Index" }
);

// MARK: Home
// [/]
app.MapControllerRoute(
    name: "Home",
    pattern: "/",
    defaults: new { controller = "Home", action = "Index" }
);

// [/promotion-program]
app.MapControllerRoute(
    name: "Promotion Program",
    pattern: "/promotion-program",
    defaults: new { controller = "Home", action = "PromotionProgram" }
);

// [/event]
app.MapControllerRoute(
    name: "Event",
    pattern: "/event",
    defaults: new { controller = "Home", action = "Event" }
);

// [/entertaiment]
app.MapControllerRoute(
    name: "Entertaiment",
    pattern: "/entertaiment",
    defaults: new { controller = "Home", action = "Entertaiment" }
);

// [/about-us]
app.MapControllerRoute(
    name: "About Us",
    pattern: "/about-us",
    defaults: new { controller = "Home", action = "AboutUs" }
);

// [/contact]
app.MapControllerRoute(
    name: "Contact",
    pattern: "/contact",
    defaults: new { controller = "Home", action = "Contact" }
);

// [/restaurant]
app.MapControllerRoute(
    name: "Restaurant",
    pattern: "/restaurant",
    defaults: new { controller = "Home", action = "Restaurant" }
);

// [/bowling]
app.MapControllerRoute(
    name: "Bowling",
    pattern: "/bowling",
    defaults: new { controller = "Home", action = "Bowling" }
);

// [/gym]
app.MapControllerRoute(
    name: "Gym",
    pattern: "/gym",
    defaults: new { controller = "Home", action = "Gym" }
);

// [/coffee]
app.MapControllerRoute(
    name: "Coffee",
    pattern: "/coffee",
    defaults: new { controller = "Home", action = "Coffee" }
);

// [/kidzone]
app.MapControllerRoute(
    name: "KidZone",
    pattern: "/kidzone",
    defaults: new { controller = "Home", action = "KidZone" }
);

// [/billiard]
app.MapControllerRoute(
    name: "Billiard",
    pattern: "/billiard",
    defaults: new { controller = "Home", action = "Billiard" }
);

// [/opera]
app.MapControllerRoute(
    name: "Opera",
    pattern: "/opera",
    defaults: new { controller = "Home", action = "Opera" }
);

// [/]
app.MapControllerRoute(
    name: "PageNotFound",
    pattern: "{*url}",
    defaults: new { controller = "Home", action = "PageNotFound" }
);

// MARK: Admin
// [/Admin]
app.MapControllerRoute(
    name: "Admin",
    pattern: "/Admin",
    defaults: new { controller = "Admin", action = "Index" }
);


app.Run();
