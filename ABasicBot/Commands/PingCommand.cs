using Discord.WebSocket;
using ShawnoStudios.Common.DiscordUtils.Commands;

namespace ABasicBot.Commands;

public class PingCommand : BotCommand
{
    public override string Command => "!ping";

    public override async Task ExecuteAsync(SocketMessage message)
    {
        await message.Channel.SendMessageAsync("Pong!");
    }
}
