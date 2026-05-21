using Discord.WebSocket;

namespace ShawnoStudios.Common.DiscordUtils.Extensions
{
    public static class DiscordSocketClientExtensions
    {
        /// <summary>
        /// Returns the Guild (Discord server) that contains the provided channel.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="channelId">The Id of the discord channel.</param>
        /// <returns></returns>
        public static SocketGuild GetGuildWithChannel(this DiscordSocketClient client, ulong channelId)
        {
            return client.Guilds.FirstOrDefault(g => g.Channels.Any(c => c.Id == channelId));
        }
    }
}
