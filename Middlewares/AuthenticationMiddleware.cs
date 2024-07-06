using Newtonsoft.Json;
using the_movie_hub.Models.Main;

namespace the_movie_hub.Middlewares
{
   public class AuthenticationMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
   {
      private readonly RequestDelegate _next = next;
      private readonly IServiceProvider _serviceProvider = serviceProvider;
      public async Task InvokeAsync(HttpContext context)
      {
         using var scope = _serviceProvider.CreateScope();
         var dbContext = scope.ServiceProvider.GetRequiredService<TheMovieHubDbContext>();
         var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();

         // get userId from the session
         string? session = httpContextAccessor.HttpContext.Session.GetString("User");
         var user = session != null ? JsonConvert.DeserializeObject<User>(session) : null;

         // required UN-AUTH for route '/login' and '/register'
         if (context.Request.Path.StartsWithSegments("/login") || context.Request.Path.StartsWithSegments("/register"))
         {
            if (user != null)
            {
               // redirect back to the home page
               context.Response.Redirect("/");
               return;
            }
         }

         // required AUTH for route '/account/*'
         if (context.Request.Path.StartsWithSegments("/account"))
         {
            if (user == null)
            {
               // redirect to the login page
               context.Response.Redirect("/Login");
               return;
            }
         }

         // required ADMIN for route '/admin/*
         if (context.Request.Path.StartsWithSegments("/admin"))
         {
            if (user == null)
            {
               // redirect to the login page
               context.Response.Redirect("/Login");
               return;
            }

            // check if the user is an admin
            if (user.Role != "admin")
            {
               // redirect to the login page
               context.Response.Redirect("/Login");
               return;
            }
         }

         await _next(context);
      }
   }
}
