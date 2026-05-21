using Discord.WebSocket;
using ShawnoStudios.Common.DiscordUtils.Commands;

namespace ABasicBot.Commands;

public class HelloCommand : BotCommand
{
    public override string Command => "!hello";

    public override async Task ExecuteAsync(SocketMessage message)
    {
        await message.Channel.SendMessageAsync($"Hey {message.Author.Username}!");
    }
}
