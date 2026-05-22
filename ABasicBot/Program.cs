using ABasicBot;
using ABasicBot.Data;
using ABasicBot.Services;
using Microsoft.EntityFrameworkCore;

var connectionString = $"Host={Environment.GetEnvironmentVariable("PGHOST")};" +
    $"Port={Environment.GetEnvironmentVariable("PGPORT") ?? "5432"};" +
    $"Database={Environment.GetEnvironmentVariable("PGDATABASE")};" +
    $"Username={Environment.GetEnvironmentVariable("PGUSER")};" +
    $"Password={Environment.GetEnvironmentVariable("PGPASSWORD")};" +
    "SSL Mode=Require;Trust Server Certificate=true";

var dbOptions = new DbContextOptionsBuilder<BotDbContext>()
    .UseNpgsql(connectionString)
    .Options;

using (var db = new BotDbContext(dbOptions))
    await db.Database.EnsureCreatedAsync();

var userService = new UserService(dbOptions);
var bot = new BasicBot(userService);

await bot.StartBotAsync();
await Task.Delay(Timeout.Infinite);
