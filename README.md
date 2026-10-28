# PublicTelegramBot

A small, open-source-ready Telegram bot written in C# and .NET 10.

## Requirements

- Visual Studio 2026 with .NET 10 SDK (or .NET 10 SDK from the command line)
- A Telegram bot created with [@BotFather](https://t.me/BotFather)

## Run in Visual Studio

1. Open `PublicTelegramBot.slnx` in Visual Studio 2026.
2. Copy `src/PublicTelegramBot/Properties/launchSettings.example.json` to
   `src/PublicTelegramBot/Properties/launchSettings.json`.
3. In your **local** `launchSettings.json`, replace `PASTE_TOKEN_HERE` with the
   token provided by @BotFather.
4. Select the `PublicTelegramBot` launch profile and press **F5**.
5. Open your bot in Telegram and send `/start`, `/help` or `/ping`.

**Do not commit `launchSettings.json`.** The `.gitignore` excludes it.

## Run with PowerShell

```powershell
$env:TELEGRAM_BOT_TOKEN = "YOUR_TOKEN"
dotnet run --project src/PublicTelegramBot/PublicTelegramBot.csproj
```

The environment variable is temporary in that PowerShell session.

## Supported commands

| Command | Description |
| --- | --- |
| `/start` | Welcome |
| `/help` | Show available commands |
| `/ping` | Check that the bot is responding |

This first version handles only private Telegram chats and runs using long polling.
The bot is online only while the program is running.

## Important security notes

- Never put bot tokens, private API keys or personal information in GitHub.
- Keep private features and private configuration out of the public repository.
- If a token is leaked, revoke/regenerate it via @BotFather.
- Use a separate test bot token while developing.

## Build

From the repository root:

```bash
dotnet restore PublicTelegramBot.slnx
dotnet build PublicTelegramBot.slnx --configuration Release --no-restore
```

No token is needed for building. A token is required only when running the bot.

## Next development steps

- Add separate command handler classes as the number of commands grows.
- Add buttons and menus.
- Add configuration and dependency injection as features grow.
- Add unit tests before implementing complex features.

The GitHub Actions workflow checks that the project restores and builds.

## License

This project is licensed under the [MIT License](LICENSE).
