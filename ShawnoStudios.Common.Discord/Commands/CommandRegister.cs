using Discord.WebSocket;
using ShawnoStudios.Common.DiscordUtils.Extensions;

namespace ShawnoStudios.Common.DiscordUtils.Commands;
public class CommandRegister
{
    public List<BotCommand> RegisteredCommands { get; set; } = new();
    public Action<BotCommand> OnCommandExecuted { get; set; }

    private readonly DiscordBot owner;

    public CommandRegister(DiscordBot owner)
    {
        this.owner = owner;
        owner.client.MessageReceived += Owner_MessageReceived;
    }

    public bool RegisterCommand<T>() where T : BotCommand, new()
    {
        if (RegisteredCommands.Any(c => c is T))
            return false;

        RegisteredCommands.Add(new T());
        return true;
    }

    public bool RegisterCommand(BotCommand command)
    {
        if (RegisteredCommands.Any(c => c.GetType() == command.GetType()))
            return false;

        RegisteredCommands.Add(command);
        return true;
    }

    private async Task Owner_MessageReceived(SocketMessage message)
    {
        foreach (var command in RegisteredCommands)
        {
            if (!command.ShouldExecuteCommand(message))
                continue;

            if (command.AllowedChannels.Any() && !command.AllowedChannels.Contains(message.Channel.Id))
                continue;

            if (command.AllowedRoles.Any())
            {
                var guild = owner.client.GetGuildWithChannel(message.Channel.Id);
                if (guild == null)
                {
                    Console.WriteLine($"Failed to find guild that has the channel id {message.Channel.Id}");
                    continue;
                }

                var user = guild.GetUser(message.Author.Id);
                if (!user.Roles.Any(r => command.AllowedRoles.Contains(r.Id)))
                {
                    await owner.ThrowErrorAsReplyAsync(message, "You don't have the required role");
                    continue;
                }
            }

            try
            {
                await command.ExecuteAsync(message);
                OnCommandExecuted?.Invoke(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing command: {ex.Message}");
            }
        }
    }
}