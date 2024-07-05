using the_movie_hub.Models.Main;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<TheMovieHubDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("azureDB"));
        options.EnableSensitiveDataLogging(false);
    }
);

// builder.Services.AddAuthentication()
// .AddGoogle(options =>
// {
//     options.ClientId = builder.Configuration["Auth:Google:ClientId"];
//     options.ClientSecret = builder.Configuration["Auth:Google:ClientSecret"];
// });

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

// use Session Middleware
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseMiddleware<AuthenticationMiddleware>();

app.MapRazorPages();

app.Run();
