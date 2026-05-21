using ABasicBot.Commands;
using ABasicBot.Services;
using Discord;
using ShawnoStudios.Common.DiscordUtils;

namespace ABasicBot;

public class BasicBot : DiscordBot
{
    public override string DiscordToken =>
        Environment.GetEnvironmentVariable("DISCORD_TOKEN")
        ?? throw new InvalidOperationException("DISCORD_TOKEN environment variable is not set.");

    public override GatewayIntents Intents =>
        GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent;

    public BasicBot(UserService userService)
    {
        client.Log += msg =>
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        };

        client.Ready += () =>
        {
            Console.WriteLine($"Logged in as {client.CurrentUser.Username}");
            return Task.CompletedTask;
        };

        client.MessageReceived += async message =>
        {
            if (message.Author.IsBot) return;
            if (!message.Content.StartsWith('!')) return;
            await userService.TrackUserAsync(message.Author);
        };

        commands.RegisterCommand(new PingCommand());
        commands.RegisterCommand(new HelloCommand());
    }
}
