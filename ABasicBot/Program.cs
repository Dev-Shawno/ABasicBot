using Discord;
using Discord.WebSocket;

var token = Environment.GetEnvironmentVariable("DISCORD_TOKEN")
    ?? throw new InvalidOperationException("DISCORD_TOKEN environment variable is not set.");

var client = new DiscordSocketClient(new DiscordSocketConfig
{
    GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
});

client.Log += msg =>
{
    Console.WriteLine(msg.ToString());
    return Task.CompletedTask;
};

client.MessageReceived += async message =>
{
    if (message.Author.IsBot) return;

    if (message.Content.Equals("!ping", StringComparison.OrdinalIgnoreCase))
        await message.Channel.SendMessageAsync("Pong!");
};

client.Ready += () =>
{
    Console.WriteLine($"Logged in as {client.CurrentUser.Username}");
    return Task.CompletedTask;
};

await client.LoginAsync(TokenType.Bot, token);
await client.StartAsync();

await Task.Delay(Timeout.Infinite);