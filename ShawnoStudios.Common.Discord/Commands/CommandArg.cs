using System.Threading.Channels;

namespace ShawnoStudios.Common.DiscordUtils.Commands;

public class CommandArg
{
    public string Command { get; set; }
    public string Value { get; set; }

    public CommandArg()
    {
    }

    public CommandArg(string arg, string argValue)
    {
        Command = arg;
        Value = argValue;
    }

    public override string ToString()
    {
        return $"Command: {Command} - Value: {Value}";
    }
}