using the_movie_hub.Models.Main;

namespace the_movie_hub.Middlewares
{
   public class AuthenticationMiddleware(RequestDelegate next, TheMovieHubDbContext dbContext, IHttpContextAccessor httpContext)
   {
      private readonly RequestDelegate _next = next;
      private readonly TheMovieHubDbContext _dbContext = dbContext;
      private readonly IHttpContextAccessor _httpContext = httpContext;

      public async Task Invoke(HttpContext context)
      {
         if (context.Request.Path.Value.Contains("/admin"))
         {
            // redirect to login page
            if (context.Session.GetString("username") == null)
            {
               context.Response.Redirect("/login");
               return;
            }
         }

         await _next(context);
      }

   }
}