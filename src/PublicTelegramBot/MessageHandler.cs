
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PublicTelegramBot;

public sealed class MessageHandler
{
    private readonly TelegramBotClient _bot;
    private readonly string? _username;

    public MessageHandler(
        TelegramBotClient bot,
        string? username)
    {
        _bot = bot;
        _username = username;
    }

    public async Task HandleAsync(
        Message message,
        UpdateType updateType)
    {
        if (updateType != UpdateType.Message ||
            message.Chat.Type != ChatType.Private ||
            message.Text is not { } text)
        {
            return;
        }

        string command = text.Split(' ', 2)[0];

        int atIndex = command.IndexOf('@');

        if (atIndex >= 0)
        {
            if (!command[(atIndex + 1)..].Equals(
                    _username,
                    StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            command = command[..atIndex];
        }

        string reply = command.ToLowerInvariant() switch
        {
            "/start" => "Hello! it's me, your personal TelegramBot",
            "/help" => "/start\n/help\n/ping",
            "/ping" => "Bot still working",
            _ when text.StartsWith('/') => "UNKNOWN COMMAND",
            _ => "MESSAGE RECEIVED"
        };

        await _bot.SendMessage(message.Chat.Id, reply);
    }
}
