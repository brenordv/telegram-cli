using Telegram.Cli.ValueObjects;

namespace Telegram.Cli.Helpers;

public static class ConfigLoader
{
    public static RuntimeConfig LoadConfig()
    {
        var token = Environment.GetEnvironmentVariable("TELEGRAM_API_TOKEN");
        ExceptionHelper.ThrowIfNull(token, "TELEGRAM_API_TOKEN");
        
        var chatId = Environment.GetEnvironmentVariable("TELEGRAM_CHAT_ID");
        ExceptionHelper.ThrowIfNull(chatId, "TELEGRAM_CHAT_ID");
        
        return new RuntimeConfig(token, chatId);
    }
}