using ABasicBot;
using ABasicBot.Data;
using ABasicBot.Services;
using Microsoft.EntityFrameworkCore;

var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL")
    ?? throw new InvalidOperationException("DATABASE_URL environment variable is not set.");

var dbOptions = new DbContextOptionsBuilder<BotDbContext>()
    .UseNpgsql(connectionString)
    .Options;

using (var db = new BotDbContext(dbOptions))
    await db.Database.EnsureCreatedAsync();

var userService = new UserService(dbOptions);
var bot = new BasicBot(userService);

await bot.StartBotAsync();
await Task.Delay(Timeout.Infinite);
