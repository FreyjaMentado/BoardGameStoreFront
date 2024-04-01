namespace CardImporter.Models;
internal class Scry_PostCard_Request
{
    public Scry_PostCard_Request() => identifiers = new List<ScryFall_CardIdentifier_Request>();
    public List<ScryFall_CardIdentifier_Request> identifiers { get; set; }
}
public record ScryFall_CardIdentifier_Request(string Name, string Set);
