namespace the_movie_hub.Models.Main;

public partial class User
{
    public required Guid Id { get; set; } = new Guid();

    public required string FullName { get; set; }

    public required string Username { get; set; }

    public required string Email { get; set; }

    public required string Phone { get; set; }

    public required string Password { get; set; }

    public string AuthType { get; set; } = "local";

    public DateOnly? Birthday { get; set; }

    public double Expended { get; set; } = 0;

    public required string CCCD { get; set; }

    public string? Avatar { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = [];

    // To String
    public override string ToString()
    {
        return $"Id: {Id}, FullName: {FullName},  Username: {Username}, Email: {Email}, Phone: {Phone}, Password: {Password}, AuthType: {AuthType}, Birthday: {Birthday}, Expended: {Expended}";
    }
}
