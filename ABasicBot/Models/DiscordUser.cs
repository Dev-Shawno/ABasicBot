namespace ABasicBot.Models;

public class DiscordUser
{
    public int Id { get; set; }
    public string DiscordId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public DateTime FirstSeen { get; set; }
    public DateTime LastSeen { get; set; }
}
