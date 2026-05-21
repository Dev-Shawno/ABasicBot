using ABasicBot.Commands;
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

    public BasicBot()
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

        commands.RegisterCommand<PingCommand>();
        commands.RegisterCommand<HelloCommand>();
    }
}
