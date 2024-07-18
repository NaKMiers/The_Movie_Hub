using the_movie_hub.Models.ViewModels;

namespace the_movie_hub.Services
{
  public interface IVnPayService
  {
    string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);
    VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
  }
}