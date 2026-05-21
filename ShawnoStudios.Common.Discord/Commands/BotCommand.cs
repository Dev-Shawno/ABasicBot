using Discord.WebSocket;

namespace ShawnoStudios.Common.DiscordUtils.Commands;
public abstract class BotCommand
{
    public abstract string Command { get; }
    public virtual List<string> Aliases { get; } = new List<string>();
    public virtual bool CanEveryoneUse { get; } = true;
    public virtual bool IsCaseSensitive { get; } = false;
    public HashSet<ulong> AllowedRoles { get; set; } = new();
    public HashSet<ulong> AllowedChannels { get; set; } = new();

    public BotCommand() { }

    public virtual bool ShouldExecuteCommand(SocketMessage message)
    {
        var fullCommand = message.Content.Trim().Split(' ')[0];

        var commandList = new List<string> { Command.ToLower() };
        commandList.AddRange(Aliases.Select(a => a.ToLower()));

        if (IsCaseSensitive)
        {
            return commandList.Contains(fullCommand);
        }
        else
        {
            return commandList.Contains(fullCommand.ToLower());
        }
    }

    protected virtual List<CommandArg> ParseArgsFromMessage(SocketMessage message)
    {
        var args = new List<CommandArg>();

        // Split the message content by space while skipping the first part (the command itself)
        var parts = message.Content.Trim().Split(' ').Skip(1).ToArray();

        for (int i = 0; i < parts.Length; i++)
        {
            args.Add(new CommandArg($"arg{i}", parts[i]));
        }

        return args;
    }

    public abstract Task ExecuteAsync(SocketMessage message);
}