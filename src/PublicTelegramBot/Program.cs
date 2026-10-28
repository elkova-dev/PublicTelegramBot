
using Telegram.Bot;
using static Telegram.Bot.TelegramBotClient;

namespace PublicTelegramBot;

internal static class Program
{
    private static async Task Main()
    {
        string? token = Environment.GetEnvironmentVariable(
            "TELEGRAM_BOT_TOKEN");

        if (string.IsNullOrWhiteSpace(token) ||
            token == "PASTE_TOKEN_HERE")
        {
            Console.WriteLine("Bot token is not configured.");
            return;
        }

        using var cancellation = new CancellationTokenSource();

        var bot = new TelegramBotClient(
            token,
            cancellationToken: cancellation.Token);

        try
        {
            var me = await bot.GetMe();

            // One handler for the entire lifetime of the bot
            var handler = new MessageHandler(bot, me.Username);

            bot.OnMessage += handler.HandleAsync;

            bot.OnError += (exception, source) =>
            {
                Console.Error.WriteLine(
                    $"Bot error ({source}): {exception.Message}");

                return Task.CompletedTask;
            };

            Console.WriteLine($"Bot @{me.Username} is running.");
            Console.WriteLine("Press Enter to stop.");
            Console.ReadLine();

            await cancellation.CancelAsync();
        }
        catch (OperationCanceledException)
            when (cancellation.IsCancellationRequested)
        {
            // Normal shutdown
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            Environment.ExitCode = 1;
        }
    }
}
