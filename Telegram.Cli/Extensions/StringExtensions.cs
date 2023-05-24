namespace Telegram.Cli.Extensions;

public static class StringExtensions
{
    public static IEnumerable<string> Split(this string message, int maxChunkSize, char wordSeparator = ' ')
    {
        if (string.IsNullOrWhiteSpace(message))
            return Array.Empty<string>();
        
        if (maxChunkSize <= 0)
            return new[] {message};
        
        var currentChunkSize = 0;
        var chunks = new List<string>();
        var result = new List<string>();

        foreach (var word in message.Split(wordSeparator))
        {
            if (currentChunkSize + word.Length + 1 > maxChunkSize)
            {
                result.AddAsSingleItem(chunks);
                chunks.Clear();
                currentChunkSize = 0;
            }

            chunks.Add(word);
            currentChunkSize += word.Length + 1;
        }

        if (chunks.Any())
            result.AddAsSingleItem(chunks);

        return result.ToArray();
    }
}