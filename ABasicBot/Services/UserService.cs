using ABasicBot.Data;
using ABasicBot.Models;
using Discord;
using Microsoft.EntityFrameworkCore;

namespace ABasicBot.Services;

public class UserService
{
    private readonly DbContextOptions<BotDbContext> _dbOptions;

    public UserService(DbContextOptions<BotDbContext> dbOptions)
    {
        _dbOptions = dbOptions;
    }

    public async Task TrackUserAsync(IUser user)
    {
        using var db = new BotDbContext(_dbOptions);
        var existing = await db.Users.FirstOrDefaultAsync(u => u.DiscordId == user.Id.ToString());

        if (existing == null)
        {
            db.Users.Add(new DiscordUser
            {
                DiscordId = user.Id.ToString(),
                Username = user.Username,
                FirstSeen = DateTime.UtcNow,
                LastSeen = DateTime.UtcNow
            });
        }
        else
        {
            existing.Username = user.Username;
            existing.LastSeen = DateTime.UtcNow;
        }

        await db.SaveChangesAsync();
    }
}
