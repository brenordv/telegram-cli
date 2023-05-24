namespace Telegram.Cli.Helpers;

public static class ExceptionHelper
{
    public static void ThrowIfNull(object obj, string name)
    {
        if (obj is string text && !string.IsNullOrWhiteSpace(text) || obj != null) return;
        throw new ArgumentNullException(name);
    }
}