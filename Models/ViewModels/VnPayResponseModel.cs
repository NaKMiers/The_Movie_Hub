namespace the_movie_hub.Models.ViewModels
{
  public class VnPaymentResponseModel
  {
    public bool Success { get; set; }
    public string PaymentMethod { get; set; }
    public string OrderDescription { get; set; }
    public string OrderId { get; set; }
    public string TransactionId { get; set; }
    public string Token { get; set; }
    public string VnPasResponseCode { get; set; }
  }

  public class VnPaymentRequestModel
  {
    public string OrderId { get; set; }
    public string FullName { get; set; }
    public string Description { get; set; }
    public double Amount { get; set; }
    public DateTime CreatedDate { get; set; }
  }
}