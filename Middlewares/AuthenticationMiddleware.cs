using the_movie_hub.Models.Main;

namespace the_movie_hub.Middlewares
{
   public class AuthenticationMiddleware(RequestDelegate next, TheMovieHubDbContext dbContext, IHttpContextAccessor httpContext)
   {
      private readonly RequestDelegate _next = next;
      private readonly TheMovieHubDbContext _dbContext = dbContext;
      private readonly IHttpContextAccessor _httpContext = httpContext;
   }
}