using ABasicBot.Models;
using Microsoft.EntityFrameworkCore;

namespace ABasicBot.Data;

public class BotDbContext : DbContext
{
    public DbSet<DiscordUser> Users { get; set; }

    public BotDbContext(DbContextOptions<BotDbContext> options) : base(options) { }
}
