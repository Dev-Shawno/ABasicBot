using Discord;
using Discord.WebSocket;
using ShawnoStudios.Common.DiscordUtils.Commands;

namespace ShawnoStudios.Common.DiscordUtils;
public abstract class DiscordBot
{
    public abstract string DiscordToken { get; }
    public virtual GatewayIntents Intents { get; } = GatewayIntents.AllUnprivileged;
    public virtual Discord.TokenType DefaultTokenType { get; set; } = Discord.TokenType.Bot;

    public readonly DiscordSocketClient client;
    protected CommandRegister commands;

    public DiscordBot()
    {
        client = new(CreateClientConfig());
        commands = new(this);
    }

    public virtual async Task StartBotAsync()
    {
        try
        {
            await client.LoginAsync(DefaultTokenType, DiscordToken);
            await client.StartAsync();
        }
        catch (Exception ex)
        {
            await Console.Out.WriteAsync($"Error starting the bot: {ex}");
        }
    }

    public async Task SendMessageAsync(ulong channelId, Embed embed, bool showInConsole = false)
    {
        var channel = await client.GetChannelAsync(channelId) as SocketTextChannel;
        if (channel != null)
        {
            if (showInConsole)
            {
                Console.WriteLine($"Sending embed to {channel.Name}");
            }

            await channel.SendMessageAsync(embed: embed);
        }
        else
        {
            if (showInConsole)
            {
                Console.WriteLine($"Failed to send embed. Channel {channelId} not found.");
            }
        }
    }

    public async Task SendMessageAsync(ISocketMessageChannel channel, string message, bool showInConsole = false)
    {
        await channel.SendMessageAsync(message);

        if (showInConsole)
            Console.WriteLine(message);
    }

    public async Task SendMessageAsync(ulong channelId, string message, bool showInConsole = false)
    {
        var channel = await client.GetChannelAsync(channelId) as SocketTextChannel;
        await SendMessageAsync(channel, message, showInConsole);
    }

    public async Task SendMessageAsync(ulong channelId, string message, bool showInConsole = false, EmbedBuilder embedBuilder = null)
    {
        var channel = await client.GetChannelAsync(channelId) as SocketTextChannel;
        if (channel != null)
        {
            if (showInConsole)
            {
                Console.WriteLine($"Sending message to {channel.Name}: {message}");
            }

            if (embedBuilder != null)
            {
                var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                var embed = embedBuilder.Build();
                await channel.SendMessageAsync(text: message, embed: embed);
            }
            else
            {
                await channel.SendMessageAsync(message);
            }
        }
        else
        {
            if (showInConsole)
            {
                Console.WriteLine($"Failed to send message. Channel {channelId} not found.");
            }
        }
    }

    public async Task ThrowErrorAsync(ISocketMessageChannel channel, string message)
    {
        message = $"Error! {message}";
        await SendMessageAsync(channel, message, showInConsole: true);
    }

    public async Task ThrowErrorAsync(ulong channelId, string message)
    {
        var channel = await client.GetChannelAsync(channelId) as SocketTextChannel;
        await ThrowErrorAsync(channel, message);
    }

    public async Task SendMessageAsReplyAsync(SocketMessage socketMessage, string message, bool showInConsole = false)
    {
        await socketMessage.Channel.SendMessageAsync(message, messageReference: new(socketMessage.Id));

        if (showInConsole)
            Console.WriteLine(message);
    }

    public async Task ThrowErrorAsReplyAsync(SocketMessage socketMessage, string message)
    {
        message = $"Error! {message}";
        await SendMessageAsReplyAsync(socketMessage, message, showInConsole: true);
    }

    protected virtual DiscordSocketConfig CreateClientConfig()
    {
        DiscordSocketConfig config = new();
        config.GatewayIntents = Intents;
        return config;
    }
}