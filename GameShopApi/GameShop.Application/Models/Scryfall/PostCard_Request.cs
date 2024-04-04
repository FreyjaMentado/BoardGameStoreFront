namespace GameShop.Application.Models.Scryfall;
public class PostCard_Request
{
    public PostCard_Request() => identifiers = new List<CardIdentifier_Request>();
    public List<CardIdentifier_Request> identifiers { get; set; }
}
public record CardIdentifier_Request(string Name, string Set);
